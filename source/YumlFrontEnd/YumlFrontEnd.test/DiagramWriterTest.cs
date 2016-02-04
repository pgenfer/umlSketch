using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuml.metadata;
using yuml.textwriter;

namespace YumlFrontEnd.test
{
    [TestFixture]
    public class DiagramWriterTest
    {
        [Test]
        public void SimpleClass_CreateText_TextCreated()
        {
            var personClass = new Class { Name = "Person" };
            var nameClass = new Class { Name = "Name" };
            var workerClass = new Class { Name = "Worker" };

            var diagram = new Diagram();
            diagram.AddNewStandAloneClass(personClass,nameClass,workerClass);

            var diagramWriter = new DiagramWriter(DiagramOptions.OnlyClasses);

            var result = diagramWriter.WriteDiagramText(diagram);
            Assert.IsTrue(result.EndsWith("[Person],[Name],[Worker]"));        
        }

        [Test]
        public void SimpleAssociation_CreateText_TextCreated()
        {
            var personClass = new Class { Name = "Person" };
            var nameClass = new Class { Name = "Name" };
            var association = new Association()
                .From(personClass)
                .To(nameClass, true, "has");
           
            var diagram = new Diagram();
            diagram.AddNewAssociation(association);

            var diagramWriter = new DiagramWriter(DiagramOptions.OnlyClasses);
            var result = diagramWriter.WriteDiagramText(diagram);
        }
    }
}
