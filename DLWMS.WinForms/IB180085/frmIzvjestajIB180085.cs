using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLWMS.WinForms.IB180085
{
    public partial class frmIzvjestajIB180085 : Form
    {
        List<StudentKonsultacijeIB180085> _lista;
        public frmIzvjestajIB180085(List<StudentKonsultacijeIB180085> lista)
        {
            InitializeComponent();
            _lista = lista;
        }

        private void frmIzvjestajIB180085_Load(object sender, EventArgs e)
        {
            var rds = new ReportDataSource();
            var tbl = new DataSet1.StudentKonsultacijeDataTable();

            foreach (var x in _lista)
            {
                var red = tbl.NewStudentKonsultacijeRow();
                red.Rb = x.Id.ToString();
                red.Predmet = x.Predmet.ToString();
                red.Vrijeme = x.VrijemeOdrzavanja.ToString();
                red.Napomena = x.Napomena.ToString();
                tbl.AddStudentKonsultacijeRow(red);
            }
            rds.Name = "DataSet1";
            rds.Value = tbl;

            var rpc = new ReportParameterCollection();
            rpc.Add(new ReportParameter("student", _lista[0].Student.ToString()));
            rpc.Add(new ReportParameter("brojZahtjeva", _lista.Count.ToString()));
            this.reportViewer1.LocalReport.SetParameters(rpc);

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.RefreshReport();
        }
    }
}
