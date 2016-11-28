using NUnit.Framework;
using Yuml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Test
{
    public class RelationTest : TestBase
    {
        [TestDescription("Render composition")]
        public void Relation_Composition_WriteTo()
        {
            var relation = new Association(
                Car, 
                Wheel, 
                RelationType.Composition, 
                startName:"has", 
                endName:4.ToString());
            
            var relationWriter = new RelationWriter(new DiagramContentMixin());
            var result = relation.WriteTo(relationWriter,DiagramDirection.LeftToRight);
            var umlText = result.Finish().ToString();

            Assert.AreEqual("[Car]has++-4[Wheel]", umlText);
        }

        [TestDescription("Render inheritance")]
        public void Relation_Inheritance_WriteTo()
        {
            var relation = new Inheritance(Car, Vehicle);

            var relationWriter = new RelationWriter(new DiagramContentMixin());
            var result = relation.WriteTo(relationWriter, DiagramDirection.LeftToRight);
            var umlText = result.Finish().ToString();

            Assert.AreEqual("[Car]-^[Vehicle]", umlText);
        }

        [TestDescription("Render association with name")]
        public void Relation_WithName_WriteTo()
        {
            var relation = new Association(Car, Wheel,RelationType.Association,"has");

            var relationWriter = new RelationWriter(new DiagramContentMixin());
            var result = relation.WriteTo(relationWriter, DiagramDirection.LeftToRight);
            var umlText = result.Finish().ToString();

            // the name will be centered within the association
            Assert.AreEqual("[Car]-has        [Wheel]", umlText);
        }
    }
}