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

        public List<string> ConsumeFields = new List<string>(); 
        public List<string> ProducesFields = new List<string>();
        public List<string> ConsumesServices = new List<string>();
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
            //var queryText =
            //    @"START n = node(*) OPTIONAL MATCH n-[r]-() WHERE (ID(n)>0 AND ID(n)<10000) DELETE n, r;";
            CurrClient.Cypher.Start(new { n = All.Nodes})
                .OptionalMatch("n-[r]-()")
                .Where("(ID(n)>0 AND ID(n)<10000)")
                .Delete("n, r")
                .ExecuteWithoutResults();

        }

        public static void AddServices(ServiceNode[] services)
        {
            ClearDB();
            foreach (ServiceNode service in services)
            {
                var newService = new
                    {
                        ID = service.ID,
                        SemanticThumbprint = service.SemanticThumbprint,
                        SemanticName = service.SemanticName
                    };
                CurrClient.Cypher.Merge("(service:Service { ID : {ID} })")
                    .OnCreate().Set("service = {newService}")
                    .WithParams(new { ID = newService.ID, newService})
                    .ExecuteWithoutResults();
                foreach (var semanticItem in service.ConsumeFields)
                {
                    var newSemanticItem = new
                        {
                            ID = semanticItem
                        };
                    CurrClient.Cypher.Merge("(semanticitem:SemanticItem { ID: {ID} })")
                        .OnCreate().Set("semanticitem = {newSemanticItem}")
                        .WithParams(new { ID = newSemanticItem.ID, newSemanticItem}).ExecuteWithoutResults();
                    // Relate together
                    CurrClient.Cypher.Match("(service:Service)", "(semanticitem:SemanticItem)")
                        .Where("service.ID = {serviceID} AND semanticitem.ID = {semanticItemID}")
                        .WithParams(new { serviceID = newService.ID, semanticItemID = newSemanticItem.ID})
                        .Create("service<-[:RequiresInput]-semanticitem")
                        .ExecuteWithoutResults();
                }
                foreach (var semanticItem in service.ProducesFields)
                {
                    var newSemanticItem = new
                    {
                        ID = semanticItem
                    };
                    CurrClient.Cypher.Merge("(semanticitem:SemanticItem { ID: {ID} })")
                        .OnCreate().Set("semanticitem = {newSemanticItem}")
                        .WithParams(new { ID = newSemanticItem.ID, newSemanticItem }).ExecuteWithoutResults();
                    // Relate together
                    CurrClient.Cypher.Match("(service:Service)", "(semanticitem:SemanticItem)")
                        .Where("service.ID = {serviceID} AND semanticitem.ID = {semanticItemID}")
                        .WithParams(new { serviceID = newService.ID, semanticItemID = newSemanticItem.ID })
                        .Create("service-[:ProvidesOutput]->semanticitem")
                        .ExecuteWithoutResults();
                }

                foreach(var consumesService in service.ConsumesServices)
                {
                    var newServiceSemanticName = new
                        {
                            SemanticName = consumesService,
                        };
                    CurrClient.Cypher.Merge("(serviceSemanticName:ServiceSemanticName { SemanticName: {SemanticName} })")
                        .OnCreate().Set("serviceSemanticName = {newServiceSemanticName}")
                        .WithParams(new { SemanticName = newServiceSemanticName.SemanticName, newServiceSemanticName })
                        .ExecuteWithoutResults();
                    CurrClient.Cypher.Match("(service:Service)", "(serviceSemanticName:ServiceSemanticName)")
                        .Where("service.ID = {serviceID} AND serviceSemanticName.SemanticName = {semanticName}")
                        .WithParams(new { serviceID = newService.ID, semanticName = newServiceSemanticName.SemanticName })
                        .Create("service<-[:RequiresService]-serviceSemanticName")
                        .ExecuteWithoutResults();
                }

                // ProducesFields self
                var serviceSemanticName = new
                {
                    SemanticName = newService.SemanticName,
                };
                CurrClient.Cypher.Merge("(serviceSemanticName:ServiceSemanticName { SemanticName: {SemanticName} })")
                    .OnCreate().Set("serviceSemanticName = {serviceSemanticName}")
                    .WithParams(new { SemanticName = serviceSemanticName.SemanticName, serviceSemanticName })
                    .ExecuteWithoutResults();
                CurrClient.Cypher.Match("(service:Service)", "(serviceSemanticName:ServiceSemanticName)")
                    .Where("service.ID = {serviceID} AND serviceSemanticName.SemanticName = {semanticName}")
                    .WithParams(new { serviceID = newService.ID, semanticName = serviceSemanticName.SemanticName })
                    .Create("service-[:ProvidesService]->serviceSemanticName")
                    .ExecuteWithoutResults();

            }
        }
    }
}
