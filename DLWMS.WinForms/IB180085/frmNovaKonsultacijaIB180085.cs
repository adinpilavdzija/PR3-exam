using DLWMS.WinForms.DB;
using DLWMS.WinForms.P5;
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
    public partial class frmNovaKonsultacijaIB180085 : Form
    {
        KonekcijaNaBazu _db = DLWMSdb.Baza;
        Student _student;
        public frmNovaKonsultacijaIB180085(Student student)
        {
            InitializeComponent();
            _student = student;
        }

        private void frmNovaKonsultacijaIB180085_Load(object sender, EventArgs e)
        {
            cmbPredmet.DataSource = _db.Predmeti.ToList();
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSacuvaj_Click(object sender, EventArgs e)
        {
            if (Validator.ValidirajKontrolu(cmbPredmet, errorProvider1, Poruke.ObaveznaVrijednost)
                && Validator.ValidirajKontrolu(dtpVrijemeKonsultacija, errorProvider1, Poruke.ObaveznaVrijednost)
                && Validator.ValidirajKontrolu(txtNapomena, errorProvider1, Poruke.ObaveznaVrijednost))
            {
                var novaKonsultacija = new StudentKonsultacijeIB180085()
                {
                    Student = _student,
                    Predmet = cmbPredmet.SelectedItem as PredmetIB180085,
                    VrijemeOdrzavanja = dtpVrijemeKonsultacija.Value,
                    Napomena = txtNapomena.Text
                };
                _db.StudentKonsultacije.Add(novaKonsultacija);
                _db.SaveChanges();
                Close();
            }
        }
    }
}
