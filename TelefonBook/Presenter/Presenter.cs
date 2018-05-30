using TelefonBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleView {
    public class Presenter {
        private Model model = new Model();
        private IView view;
        public Presenter(IView view) {
            this.view = view;
            this.view.SetShowAllContacts += new EventHandler<EventArgs>(OnSetShowAllContacts);
            this.view.SetNewContact += new EventHandler<EventArgs>(OnSetNewContact);
            this.view.SetFindContacts += new EventHandler<EventArgs>(OnSetFindContacts);
            this.view.SetDeleteContacts += new EventHandler<EventArgs>(OnSetDeleteContacts);
            this.view.SetInitTestContacts += new EventHandler<EventArgs>(OnSetInitTestContacts);
        }

        private void OnSetShowAllContacts(object sender, EventArgs e) {
            var temp = new List<string[]>();
            foreach (var emp in model.ShowAll()) {
                temp.Add(new string[] { emp.Id.ToString(), emp.LastName, emp.FirstName, emp.ExtensionPhone.Number, emp.Position.Name });
            }
            view.ShowListContact(temp);
        }
        private void OnSetNewContact(object sender, EventArgs e) {
            model.New(new Employee() { LastName =view.InputContact[0],
                FirstName = view.InputContact[0],
                MiddleName = view.InputContact[0],
                NumberHomePhone = view.InputContact[0],
                ExtensionPhone = new ExtensionPhone() {
                    Number = view.InputContact[0],
                    InstallationSite = view.InputContact[0],
                },
                Position = new Position (){
                    Name = view.InputContact[0],
                    Subdivison = new Subdivison() {
                        Name = view.InputContact[0],
                        Divison = new Divison() {
                            Name = view.InputContact[0]
                        }
                    }
                }
            });
        }
        private void OnSetFindContacts(object sender, EventArgs e) {
            var temp = new List<string[]>();
            foreach (var emp in model.Find(view.InputFindContact)) {
                temp.Add(new string[] { emp.Id.ToString(), emp.LastName, emp.FirstName, emp.ExtensionPhone.Number, emp.Position.Name });
            }
            view.ShowListContact(temp);
        }
        private void OnSetDeleteContacts(object sender, EventArgs e) {
            model.Delete(view.InputFindContact);
        }
        private void OnSetInitTestContacts(object sender, EventArgs e) {
            model.InitTest();
        }



    }
}
