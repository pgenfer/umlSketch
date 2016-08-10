using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Yuml.Command;
using YumlFrontEnd.editor;
using static NSubstitute.Substitute;
using NSubstitute;

namespace Yuml.Test.ViewModel
{
    public class SingleItemViewModelTest : TestBase
    {
        [Test]
        public void CanInstantiateSingleItemViewModel()
        {
            var validateNameService = For<IValidateNameService>();
            validateNameService.ValidateNameChange(
                Arg.Any<string>(),
                Arg.Any<string>())
                .Returns(new Success());
            var commands = For<ISingleClassifierCommands>();

            var viewModel = new ClassifierViewModel(commands);
            var initializedViewModel = viewModel.Init(Integer);

            Assert.IsNotNull(initializedViewModel);
        }
    }
}
