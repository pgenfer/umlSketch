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

        [TestDescription("Name is not set if invalid")]
        public void StopEditingTest_Dont_Set_Invalid_Name()
        {
            _renameCommand
                .CanRenameWith(Arg.Any<string>())
                .ReturnsForAnyArgs(new Error(string.Empty));

            // set initial name
            _editableNameMixin.Name = "old";
            // start editing, name is invalid, so it won't be set
            _editableNameMixin.StartEditing();
            _editableNameMixin.Name = "new";
            _editableNameMixin.StopEditing(Confirmation.Confirmed);
            // ensure that the name remains the same if invalid
            Assert.AreEqual("old",_editableNameMixin.Name);
        }

        [TestDescription("Name is set if valid")]
        public void StopEditingTest_Set_valid_Name()
        {
            _renameCommand
                .CanRenameWith(Arg.Any<string>())
                .ReturnsForAnyArgs(new Success());

            // set initial name
            _editableNameMixin.Name = "old";
            // start editing, name is invalid, so it won't be set
            _editableNameMixin.StartEditing();
            _editableNameMixin.Name = "new";
            _editableNameMixin.StopEditing(Confirmation.Confirmed);
            // ensure that the name remains the same if invalid
            Assert.AreEqual("new", _editableNameMixin.Name);
        }
    }
}

