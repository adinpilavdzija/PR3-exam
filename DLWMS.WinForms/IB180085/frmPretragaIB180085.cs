using DLWMS.WinForms.DB;
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
    public partial class frmPretragaIB180085 : Form
    {
        KonekcijaNaBazu _db = DLWMSdb.Baza;
        string filter = "";
        int godina;
        List<Student> lista;
        public frmPretragaIB180085()
        {
            InitializeComponent();
            dgvPretraga.AutoGenerateColumns = false;
        }

        private void frmPretragaIB180085_Load(object sender, EventArgs e)
        {
            foreach (var s in _db.Studenti)
            {
                s.ImePrezime = s.ToString();
                s.ProsjecnaOcjena();
            }
            cmbGodinaStudija.SelectedIndex = 0;
            UcitajPodatke();
        }

        private void UcitajPodatke()
        {
            dgvPretraga.DataSource = null;
            //lista = new List<Student>();
            if (cmbGodinaStudija.SelectedIndex == 0)
                lista = _db.Studenti.Where(x => x.Ime.ToLower().Contains(filter) || x.Prezime.ToLower().Contains(filter) || filter == "").ToList();
            else
                lista = _db.Studenti.Where(x => (x.Ime.ToLower().Contains(filter) || x.Prezime.ToLower().Contains(filter) || filter == "") && x.GodinaStudija == godina).ToList();

            dgvPretraga.DataSource = lista;
        }

        private void cmbGodinaStudija_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGodinaStudija.SelectedIndex != 0)
                godina = int.Parse(cmbGodinaStudija.Text);
            UcitajPodatke();
        }

        private void txtImePrezime_TextChanged(object sender, EventArgs e)
        {
            filter = txtImePrezime.Text.ToLower();
            UcitajPodatke();
        }

        private void dgvPretraga_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                var formaKonsultacije = new frmKonsultacijeIB180085(dgvPretraga.SelectedRows[0].DataBoundItem as Student);
                formaKonsultacije.ShowDialog();
            }
        }


    }
}
