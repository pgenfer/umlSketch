using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NSubstitute;
using NUnit.Framework;
using Yuml.Commands;
using YumlFrontEnd.editor;
using static NSubstitute.Substitute;


namespace Yuml.Test
{
    public class EditableMixinTest : SimpleTestBase
    {
        private EditableNameMixin _editableNameMixin;
        private IRenameCommand _renameCommand;
        private IValidateNameService _validationService;

        [SetUp]
        public void Init()
        {
            _renameCommand = For<IRenameCommand>();
            _validationService = For<IValidateNameService>();
            _editableNameMixin = new EditableNameMixin(_validationService, _renameCommand);
        }

        [TestDescription("Rename command is called after name change")]
        public void StopEditing_RenameCalled()
        {
            _validationService
                .ValidateNameChange(
                    Arg.Any<string>(),
                    Arg.Any<string>())
                .ReturnsForAnyArgs(new Success());

            var newName = RandomString();
            _editableNameMixin.Name = RandomString();
            _editableNameMixin.StartEditing();
            _editableNameMixin.Name = newName;
            _editableNameMixin.StopEditing(Confirmation.Confirmed);

            // ensure that rename was called
            _renameCommand.Received().Do(newName);
        }

        [TestDescription("Edit operation canceled, rename is not called")]
        public void StopEditing_RenameCanceled()
        {
            _validationService
               .ValidateNameChange(
                   Arg.Any<string>(),
                   Arg.Any<string>())
               .ReturnsForAnyArgs(new Success());

            var oldName = RandomString(1);
            var newName = RandomString(2);
            _editableNameMixin.Name = oldName;
            _editableNameMixin.StartEditing();
            _editableNameMixin.Name = newName;
            _editableNameMixin.StopEditing(Confirmation.Canceled);

            // ensure that rename was not called
            _renameCommand.DidNotReceive().Do(newName);
            // and that the name was not changed
            Assert.AreEqual(_editableNameMixin.Name,oldName);
        }
    }
}

