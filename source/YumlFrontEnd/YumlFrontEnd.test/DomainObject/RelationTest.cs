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
            var startNode = new StartNode
            {
                Name = "has",
                Classifier = Car,
                IsNavigatable = false
            };
            var endNode = new EndNode
            {
                Name = 4.ToString(),
                Classifier = Wheel,
                IsNavigatable = true
            };

            var relation = new Relation
            {
                Start = startNode,
                End = endNode,
                Type = RelationType.Composition
            };

            var relationWriter = new RelationWriter(new DiagramContentMixin());
            var result = relation.WriteTo(relationWriter);
            var umlText = result.Finish().ToString();

            Assert.AreEqual("[Car]has++-4>[Wheel]", umlText);
        }

        [TestDescription("Render inheritance")]
        public void Relation_Inheritance_WriteTo()
        {
            var startNode = new StartNode { Classifier = Car};
            var endNode = new EndNode{Classifier = Vehicle};

            var relation = new Relation
            {
                Start = startNode,
                End = endNode,
                Type = RelationType.Inheritance
            };

            var relationWriter = new RelationWriter(new DiagramContentMixin());
            var result = relation.WriteTo(relationWriter);
            var umlText = result.Finish().ToString();

            Assert.AreEqual("[Car]-^[Vehicle]", umlText);
        }
    }
}