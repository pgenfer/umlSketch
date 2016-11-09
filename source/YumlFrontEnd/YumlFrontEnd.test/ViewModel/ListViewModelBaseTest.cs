//using NUnit.Framework;
//using YumlFrontEnd.editor;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NSubstitute;
//using Yuml;
//using Yuml.Command;
//using Yuml.Test;
//using static NSubstitute.Substitute;
//using ListViewModelMock = YumlFrontEnd.editor.ListViewModelBase<Common.IVisible,object>;
//using SingleViewModelStub = YumlFrontEnd.editor.SingleItemViewModelBase<object>;

//namespace YumlFrontEnd.Test
//{
//    public class ListViewModelBaseTest : TestBase
//    {
//        private ListViewModelMock _listViewModelMock;
//        private SingleViewModelStub _singleViewModelStub;

//        protected override void Init()
//        {
//            var listViewCommands = For<IListCommandContext<object>>();
//            var singleViewCommands = For<ISingleCommandContext>();
//            _listViewModelMock = For<ListViewModelMock>(listViewCommands);
//            _singleViewModelStub = For<SingleViewModelStub>(singleViewCommands);
//        }

//        [TestDescription("Check that expander is hidden if item list is empty")]
//        public void RemoveItemTest()
//        {
//            // add a single item to the list
//            _listViewModelMock.Items.Add(_singleViewModelStub);
//            // remove the item
//            _listViewModelMock.RemoveItem(_singleViewModelStub);
//            // ensure the CanExpand property notification is fired (list is empty, expander should be hidden)
//            _listViewModelMock.Received().NotifyOfPropertyChange("CanExpand");
//        }
//    }
//}