using NUnit.Framework;
using YumlFrontEnd.editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Yuml.Command;
using Yuml.Test;
using static NSubstitute.Substitute;

namespace YumlFrontEnd.editor.Test
{
    public class SelectClassifierWithNullItemMixinTest : SimpleTestBase
    {
        private IChangeTypeToNullCommand _command;
        ClassifierSelectionItemsSource _source;
        private SelectClassifierWithNullItemMixin _selectClassMixin;
        private const string Dummy = nameof(Dummy);
        private readonly ClassifierItemViewModel _dummyClassifier = new ClassifierItemViewModel(Dummy);

        protected override void Init()
        {
            _command = For<IChangeTypeToNullCommand>();
            _source = For<ClassifierSelectionItemsSource>();
            _selectClassMixin = new SelectClassifierWithNullItemMixin(_source, _command);
        }

        [TestDescription("Check that null item is created if no classifier name is used")]
        public void SelectClassifierByNameTest()
        {
            _selectClassMixin.SelectClassifierByName(null);
            Assert.AreEqual(_selectClassMixin.SelectedClassifier, ClassifierItemViewModel.None);
        }

        [TestDescription("Check that correct command method is called")]
        public void SelectClassifier_NewClassifierSet()
        {
            // Arrange, no classifier was set
            _selectClassMixin.SelectedClassifier = ClassifierItemViewModel.None;
            // Act
            _selectClassMixin.SelectedClassifier = _dummyClassifier;
            // Assert: command was called correctly
            _command.Received().SetNewType(Dummy);
        }

        [TestDescription("Check that ClearType command is called")]
        public void SelectClassifier_ClassifierRemoved()
        {
            // Arrange, no classifier was set
            _selectClassMixin.SelectedClassifier = _dummyClassifier;
            // Act
            _selectClassMixin.SelectedClassifier = ClassifierItemViewModel.None;
            // Assert: command was called correctly
            _command.Received().ClearType(Dummy);
        }

        [TestDescription("Check that Type Changed command is called")]
        public void SelectClassifier_ClassifierChanged()
        {
            const string dummy2 = nameof(dummy2);
            // Arrange, no classifier was set
            _selectClassMixin.SelectedClassifier = _dummyClassifier;
            // Act
            _selectClassMixin.SelectedClassifier = new ClassifierItemViewModel(dummy2);
            // Assert: command was called correctly
            _command.Received().ChangeType(Dummy, dummy2);
        }
    }
}