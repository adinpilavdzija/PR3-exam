using DLWMS.WinForms.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLWMS.WinForms.IB180085
{
    public partial class frmKonsultacijeIB180085 : Form
    {
        KonekcijaNaBazu _db = DLWMSdb.Baza;
        Student _student;
        List<StudentKonsultacijeIB180085> lista = new List<StudentKonsultacijeIB180085>();
        public frmKonsultacijeIB180085(Student student)
        {
            InitializeComponent();
            dgvKonsultacija.AutoGenerateColumns = false;
            _student = student;
        }

        private void frmKonsultacijeIB180085_Load(object sender, EventArgs e)
        {
            label1.Text = $"Lista zahtjeva za konsultacije studenta: {_student}";
            UcitajPodatke();
            cmbPredmet.DataSource = _db.Predmeti.ToList();
        }

        private void UcitajPodatke()
        {
            lista = _db.StudentKonsultacije.Where(x => x.Student.Id == _student.Id).ToList();
            dgvKonsultacija.DataSource = lista;
            Text = $"Broj zapisa konsultacija: {dgvKonsultacija.Rows.Count}";
        }

        private void btnDodajZahtjev_Click(object sender, EventArgs e)
        {
            var formaNovaKonsultacija = new frmNovaKonsultacijaIB180085(_student);
            formaNovaKonsultacija.ShowDialog();
            UcitajPodatke();
        }

        private void dgvKonsultacija_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var red = dgvKonsultacija.SelectedRows[0].DataBoundItem as StudentKonsultacijeIB180085;
            if (e.ColumnIndex == 4)
            {
                if (red.VrijemeOdrzavanja.Date > DateTime.Now.Date)
                {
                    if (MessageBox.Show("Da li zelite obrisati oznaceni red?", "Obavjestenje", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _db.StudentKonsultacije.Remove(red);
                        _db.SaveChanges();
                        UcitajPodatke();
                    }
                }
                else
                    MessageBox.Show("Nije moguce izbrisati vec realizovanu konsultaciju", "Obavjestenje", MessageBoxButtons.OK);
            }
        }

        private void btnPrintaj_Click(object sender, EventArgs e)
        {
            var formaIzvjestaj = new frmIzvjestajIB180085(lista);
            formaIzvjestaj.ShowDialog();
        }

        private void btnDodajZahtjevDole_Click(object sender, EventArgs e)
        {
            if (txtBrojZahtjeva.Text != "")
            {
                int brojDodanih = int.Parse(txtBrojZahtjeva.Text);
                var predmet = cmbPredmet.SelectedItem as PredmetIB180085;

                string dodavanja = "";
                Thread baza = new Thread(() =>
                {
                    dodavanja = GenerisiPolozene(brojDodanih, predmet);
                });
                baza.Start();
                baza.Join();
                txtInfo.Text = dodavanja;
                UcitajPodatke();
            }
        }

        private string GenerisiPolozene(int brojDodanih, PredmetIB180085 predmet)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            string dodavanja = "";
            for (int i = 0; i < brojDodanih; i++)
            {
                var novaKonsultacija = new StudentKonsultacijeIB180085()
                {
                    Student = _student,
                    Predmet = predmet,
                    VrijemeOdrzavanja = DateTime.Now,
                    Napomena = $"Napomena: {_student.Id}",
                };
                _db.StudentKonsultacije.Add(novaKonsultacija);
                dodavanja +=
                    $"Za {_student} dodat zahtjev za konsultacije -> {predmet} {DateTime.Now} {Environment.NewLine}";
            }

            _db.SaveChanges();
            Thread.Sleep(500);
            MessageBox.Show($"Uspjesno dodane {brojDodanih} konsultacije", "Uspjeh", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return dodavanja;
        }
    }
}
