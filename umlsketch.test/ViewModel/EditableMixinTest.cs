using NSubstitute;
using NUnit.Framework;
using UmlSketch.Command;
using UmlSketch.Editor;
using UmlSketch.Validation;
using static NSubstitute.Substitute;


namespace UmlSketch.Test
{
    public class EditableMixinTest : SimpleTestBase
    {
        private EditableNameMixin _editableNameMixin;
        private IRenameCommand _renameCommand;
        

        protected override void Init()
        {
            _renameCommand = For<IRenameCommand>();
            _editableNameMixin = new EditableNameMixin(_renameCommand);
        }

        [TestDescription("Rename command is called after name change")]
        public void StopEditing_RenameCalled()
        {
            _renameCommand
                .CanRenameWith(Arg.Any<string>())
                .ReturnsForAnyArgs(new Success());

            var newName = RandomString(1);
            _editableNameMixin.Name = RandomString(2);
            _editableNameMixin.StartEditing();
            _editableNameMixin.Name = newName;
            _editableNameMixin.StopEditing(Confirmation.Confirmed);

            // ensure that rename was called
            _renameCommand.Received().Rename(newName);
        }

        [TestDescription("Edit operation canceled, rename is not called")]
        public void StopEditing_RenameCanceled()
        {
            _renameCommand
                .CanRenameWith(Arg.Any<string>())
                .ReturnsForAnyArgs(new Success());

            var oldName = RandomString(1);
            var newName = RandomString(2);
            _editableNameMixin.Name = oldName;
            _editableNameMixin.StartEditing();
            _editableNameMixin.Name = newName;
            _editableNameMixin.StopEditing(Confirmation.Canceled);

            // ensure that rename was not called
            _renameCommand.DidNotReceive().Rename(newName);
            // and that the name was not changed
            Assert.AreEqual(_editableNameMixin.Name,oldName);
        }
    }
}

