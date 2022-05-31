using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Motorsazan.CustomersApp.Shared.Models.Input.ProductDetail;
using Motorsazan.CustomersApp.Shared.Models.Output.ProductDetail;
using Motorsazan.CustomersApp.Shared.Utilities;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using Syncfusion.SfPdfViewer.XForms;
using Xamarin.Forms;
using static Motorsazan.CustomersApp.Views.Pages.PdfViewer;

namespace Motorsazan.CustomersApp.ViewModels
{
    public class PdfViewerViewModel : INotifyPropertyChanged
    {
        private Stream m_pdfDocumentStream;
        Command<object> saveCommand;
        private string _catalog;
        private string _fileName;

        public Command<object> SaveCommand
        {
            get { return saveCommand; }
            protected set { saveCommand = value; }
        }
        /// <summary>
        /// An event to detect the change in the value of a property.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The PDF document stream that is loaded into the instance of the PDF viewer. 
        /// </summary>
        public Stream PdfDocumentStream
        {
            get
            {
                return m_pdfDocumentStream;
            }
            set
            {
                m_pdfDocumentStream = value;
                NotifyPropertyChanged("PdfDocumentStream");
            }
        }

        /// <summary>
        /// Constructor of the view model class
        /// </summary>
        public PdfViewerViewModel()
        {
            PdfDocumentStream = InputPdfViewer.MemoryArray;
            saveCommand = new Command<object>(OnDocumentSaved);
        }

        public void OnDocumentSaved(object obj)
        {
            Stream strm = (obj as DocumentSaveInitiatedEventArgs).SaveStream;
            string filePath = DependencyService.Get<ISave>().Save(strm as MemoryStream);
            string message = "The PDF has been saved to " + filePath;
            DependencyService.Get<IAlertView>().Show(message);
        }


        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public  Task<OutputGetProductDetailListByProductId[]> GetProductDetailListByProductId(long ProductionId)
        {
            var url = $"{Settings.BaseUrl}/ProductDetail/GetProductDetailListByProductId";
            var input = new InputGetProductDetailListByProductId
            {
                ProductionId = ProductionId
            };
            var source =  ApiConnector<OutputGetProductDetailListByProductId[]>.Post(url, input);

            return source;
        }


    }

}
