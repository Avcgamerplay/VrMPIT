using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hakatonv2
{
    public partial class PatientSeansInfoCard : UserControl
    {
        private Session session;

        public event EventHandler<Session> SessionClicked;

        private Size originalSize = new Size(180, 80);
        private Size hoverSize = new Size(182, 82);

        public PatientSeansInfoCard(Session session)
        {
            this.session = session;
            InitializeComponent();
            InitCard();
        }

        void InitCard()
        {
            Console.WriteLine($"{session.DoctorNotes} fsdfsdf");
            string[] lastNotes = session.DoctorNotes.Split('/');

            Console.WriteLine($"{lastNotes.Length} fsdfsdf");

            this.Size = originalSize;

            if (lastNotes.Length > 0) notes_label.Text = lastNotes[lastNotes.Length - 1];
            else notes_label.Text = "Заметок ещё нет";

            label1.Text = "Сеанс: " + session.Date;

            this.Cursor = Cursors.Hand;
            this.BackColor = Color.White;

            this.Click += (s, e) => SessionClicked?.Invoke(this, session);

            AddClickHandlerToChildren(this);
        }

        private void AddClickHandlerToChildren(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                control.Click += (s, e) => SessionClicked?.Invoke(this, session);
                control.Cursor = Cursors.Hand;

                control.MouseEnter += (s, e) => this.Size = hoverSize;
                control.MouseLeave += (s, e) => this.Size = originalSize;

                // Рекурсивно для вложенных контролов
                if (control.HasChildren)
                {
                    AddClickHandlerToChildren(control);
                }
            }
        }

    }
}
