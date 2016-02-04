using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuml.mixins;

namespace yuml.metadata
{
    /// <summary>
    /// A class diagram stores a list of associations
    /// and a list of classes that are not part of an
    /// association
    /// </summary>
    public class Diagram
    {
        public Diagram()
        {
            InitSystemTypes();
        }
        
        /// <summary>
        /// stores all associations available in the diagram.
        /// An association always connects two classes with each other
        /// </summary>
        private AssociationList _associations = new AssociationList();
        /// <summary>
        /// the class list stores all classes that are not part of an association
        /// </summary>
        private ClassList _classes = new ClassList();
        private ClassList _systemTypes = new ClassList();

        public void AddNewStandAloneClass(params Class[] @classes)
        {
            foreach (var @class in classes) _classes.AddClass(@class);
        }

        public void AddNewAssociation(Association association)
        {
            // first remove both classes from the 
            // list of stand alone classes since they will be stored
            // with their association now
            _classes.RemoveClass(association.FirstConnection.Class);
            _classes.RemoveClass(association.SecondConnection.Class);

            _associations.AddAssociation(association);
        }

        private void InitSystemTypes()
        {
            var systemTypeNames = new[]
            {
                "int","string","float","double","DateTime","TimeSpan","object","struct"
            };
            foreach (var systemTypeName in systemTypeNames) _systemTypes.AddClass(Class.CreateSystemType(systemTypeName));
        }

        public IEnumerable<Class> Classes => _classes.Classes;
        public IEnumerable<Association> Associations => _associations.Associations;
    }
}
