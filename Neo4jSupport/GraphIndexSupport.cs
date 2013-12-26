using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace Neo4jSupport
{
    public class ServiceNode
    {
        public string SemanticThumbprint { get; set; }
        public string SemanticName { get; set; }
        public string ID { get; set; }

        public List<SemanticItemNode> Consumes = new List<SemanticItemNode>(); 
    }

    public class ServiceConsumes : Relationship, IRelationshipAllowingSourceNode<ServiceNode>,
        IRelationshipAllowingTargetNode<SemanticItemNode>
    {
        public ServiceConsumes(NodeReference targetNode) : base(targetNode)
        {
        }

        public ServiceConsumes(NodeReference targetNode, object data) : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return "SERVICE_CONSUMES"; }
        }
    }

    public class SemanticItemNode
    {
        public string SemanticItemValue { get; set; }
    }

    public class GraphIndexSupport
    {
        public static GraphClient CurrClient;
        static GraphIndexSupport()
        {
            CurrClient = new GraphClient(new Uri("http://localhost:7474/db/data"));
            CurrClient.Connect();
        }

        public static void ClearDB()
        {
        }

        public static void AddServices(ServiceNode[] services)
        {
            foreach (ServiceNode service in services)
            {
                var newService = new
                    {
                        ID = service.ID,
                        SemanticThumbprint = service.SemanticThumbprint,
                        SemanticName = service.SemanticName
                    };
                CurrClient.Cypher.Create("(service:Service {newService})")
                    .WithParam("newService", newService).ExecuteWithoutResults();
                foreach (var semanticItem in service.Consumes)
                {
                    var newSemanticItem = new
                        {
                            ID = semanticItem.SemanticItemValue
                        };
                    CurrClient.Cypher.Merge("(semanticitem:SemanticItem { ID: {ID} })")
                        .OnCreate().Set("semanticitem = {newSemanticItem}")
                        .WithParams(new { ID = newSemanticItem.ID, newSemanticItem}).ExecuteWithoutResults();
                    // Relate together
                    CurrClient.Cypher.Match("(service:Service)", "(semanticitem:SemanticItem)")
                        .Where("service.ID = {serviceID} AND semanticitem.ID = {semanticItemID}").
                        WithParams(new { serviceID = newService.ID, semanticItemID = newSemanticItem.ID})
                        .Create("service-[:CONSUMES]->semanticitem")
                        .ExecuteWithoutResults();
                }
            }
        }
    }
}
