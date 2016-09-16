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
            var relation = new Relation(Car, Wheel, RelationType.Composition, "has", 4.ToString());
            
            var relationWriter = new RelationWriter(new DiagramContentMixin());
            var result = relation.WriteTo(relationWriter);
            var umlText = result.Finish().ToString();

            Assert.AreEqual("[Car]has++-4[Wheel]", umlText);
        }

        [TestDescription("Render inheritance")]
        public void Relation_Inheritance_WriteTo()
        {
            var relation = new Relation(Car, Vehicle, RelationType.Inheritance);

            var relationWriter = new RelationWriter(new DiagramContentMixin());
            var result = relation.WriteTo(relationWriter);
            var umlText = result.Finish().ToString();

            Assert.AreEqual("[Car]-^[Vehicle]", umlText);
        }
    }
}