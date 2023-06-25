using RAI_MVC.Models;
using RAI_MVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Spire.Doc;

namespace RAI_MVC.Controllers
{
    public class RAIController : Controller
    {
        private UsersRepository _usersRepository = null;
        private LoanRepository _loanRepository = null;
        public RAIController()
        {
            _usersRepository = new UsersRepository();
            _loanRepository = new LoanRepository();
        }
        public ActionResult Detail(int? id)
        {
            //if (userID == null)
            //{
            //    return HttpNotFound();
            //}
            var user = _loanRepository.GetLoan(id.Value);
            
            return View(user);
        }
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Loan loan = _loanRepository.GetLoans
        //    if (loan == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(loan);
        //}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //_loanRepository.DeleteLoan(id);

            //TempData["Message"] = "Loan Successfully Deleted";
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Loan loan = _loanRepository.GetLoan((int)id);
            if (loan == null)
            {
                return HttpNotFound();
            }


            SetupSelectListItems();
            return View(loan);
        }
        [HttpPost]
        public ActionResult Edit(Loan loan)
        {
            ValidateLoan(loan);
            if (ModelState.IsValid)
            {
                _loanRepository.UpdateLoan(loan);

                TempData["Message"] = "Loan Successfully Updated";
                return RedirectToAction("Index");
            }

            SetupSelectListItems();
            return View(loan);
        }

        public ActionResult Index(string SearchText, string StatusSelectListItems, string EntitiesSelectListItems, string ClientsSelectListItems, string chkShowCompleted, string submitButton)
        {
           var raiLoans = _loanRepository.GetLoans();
            SetupSelectListItems();
            return View(raiLoans);
        }

        public ActionResult SubmitSelected(string loanlist)
        {
            try
            {

                //ErrorLabel.Content = "";

                List<Loan> loans = new List<Loan>();

                //for (int i = 0; i < GridView1.Rows.Count; i++)
                //{
                //    CheckBox chkIsChecked = (CheckBox)GridView1.Rows[i].FindControl("chkIsChecked");
                //    int loanID = Convert.ToInt16(GridView1.DataKeys[i].Value);
                //    if (chkIsChecked.Checked)
                //    {
                //        TableFundingLoan loan = new TableFundingLoan(loanID);
                //        loan.LoanUW = new TableFundingLoanUW(loan.LoanID);
                //        loan.LoanFunding = new TableFundingLoanFunding(loan.LoanID);
                //        loans.Add(loan);
                //    }
                //}
                Loan loan = _loanRepository.GetLoan(1);
                loans.Add(loan);

                if (loans.Count() == 0)
                {
                    //ErrorLabel.Content = "Please select at least one loan";
                    //ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);
                    //return;
                }


                Client client = _loanRepository.GetClient(loans[0].ClientID.Value);
                Entity entity = _loanRepository.GetEntity(loans[0].EntityID.Value);

                Spire.Doc.Document document = new Spire.Doc.Document();
                //Add Section

                Spire.Doc.Section section1 = document.AddSection();

                section1.PageSetup.Orientation = Spire.Doc.Documents.PageOrientation.Portrait;

                //Add Paragraph

                Spire.Doc.Documents.Paragraph parExhibitA = section1.AddParagraph();


                parExhibitA.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                //Add Paragraph

                Spire.Doc.Documents.Paragraph parSchedofInv = section1.AddParagraph();
                parSchedofInv.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

                Spire.Doc.Documents.Paragraph parDate = section1.AddParagraph();
                parDate.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

                Spire.Doc.Table tblInv = section1.AddTable(true);
                Spire.Doc.Fields.TextRange trExhibitA = parExhibitA.AppendText("EXHIBIT A\v");
                trExhibitA.CharacterFormat.UnderlineStyle = Spire.Doc.Documents.UnderlineStyle.Single;


                parSchedofInv.AppendText("SCHEDULE OF ASSETS\v");

                parDate.AppendText("Date:" + DateTime.Now.ToShortDateString() + "\v");

                //Create Header and Data

                String[] Header = { "Loan ID", "Borrower", "Address" };

                //
                // Table Logic
                //
                //Add Cells
                tblInv.ResetCells(loans.Count() + 1, Header.Length);
                //Header Row
                Spire.Doc.TableRow FRow = tblInv.Rows[0];
                FRow.IsHeader = true;

                //Row Height
                FRow.Height = 23;

                //Header Format
                //FRow.RowFormat.BackColor = Color.AliceBlue;

                for (int i = 0; i < Header.Length; i++)
                {
                    //Cell Alignment
                    Spire.Doc.Documents.Paragraph p = FRow.Cells[i].AddParagraph();
                    FRow.Cells[i].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    //Data Format
                    Spire.Doc.Fields.TextRange TR = p.AppendText(Header[i]);
                    //TR.CharacterFormat.FontName = "Calibri";
                    TR.CharacterFormat.FontSize = 14;
                    //TR.CharacterFormat.TextColor = Color.Teal;                
                    TR.CharacterFormat.Bold = true;

                }

                //Data Row

                string loanNumberList = "";
                int r = 0;
                //for (int r = 0; r < data.Length; r++)
                foreach (Loan rptloan in loans)
                {

                    Spire.Doc.TableRow DataRow = tblInv.Rows[r + 1];

                    //Row Height

                    DataRow.Height = 20;

                    //C Represents Column.

                    //for (int c = 0; c < data[r].Length; c++)                    
                    //{                    
                    //Cell Alignment
                    DataRow.Cells[0].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[1].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[2].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;

                    //Fill Data in Rows

                    loanNumberList += loan.LoanNumber + " "; ;

                    Spire.Doc.Documents.Paragraph pLoanNumber = DataRow.Cells[0].AddParagraph();
                    Spire.Doc.Fields.TextRange TRLoanNumber = pLoanNumber.AppendText(loan.LoanNumber);

                    Spire.Doc.Documents.Paragraph pBorrower = DataRow.Cells[1].AddParagraph();
                    Spire.Doc.Fields.TextRange TRBorrower = pBorrower.AppendText(loan.LoanMortgagee);

                    Spire.Doc.Documents.Paragraph pAddress = DataRow.Cells[2].AddParagraph();
                    Spire.Doc.Fields.TextRange TRAddress = pAddress.AppendText(loan.LoanPropertyAddress);

                    //Format Cells

                    pLoanNumber.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pBorrower.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pAddress.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

                    //TRLoanNumber.CharacterFormat.FontName = "Calibri";
                    //TRBorrower.CharacterFormat.FontName = "Calibri";
                    //TRAddress.CharacterFormat.FontName = "Calibri";

                    TRLoanNumber.CharacterFormat.FontSize = 12;
                    TRBorrower.CharacterFormat.FontSize = 12;
                    TRAddress.CharacterFormat.FontSize = 12;

                    //TR2.CharacterFormat.TextColor = Color.Brown;

                    //}
                    r++;

                }
                //
                // End Table Logic
                //
                Spire.Doc.Documents.Paragraph parWireInfo = section1.AddParagraph();
                parWireInfo.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;

                if (loans[0].BaileeLetterDate == null)
                {
                    //ErrorLabel.Content = "Bailee Letter Date Missing";
                    //ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);

                    TempData["Message"] = "Bailee Letter Date Missing";
                    return RedirectToAction("Index");
                }
                if (loans[0].ClosingDate == null)
                {
                    //ErrorLabel.Content = "Closing Date Missing";
                    //ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);
                    TempData["Message"] = "Closing Date Date Missing";
                    return RedirectToAction("Index");
                }
                DateTime baileeLetterDate = (DateTime)loans[0].BaileeLetterDate;
                DateTime closingDate = (DateTime)loans[0].ClosingDate;

                parWireInfo.AppendText("Purchase Advice Date:" + baileeLetterDate.ToShortDateString() + "\v\v");
                parWireInfo.AppendText("Closing Date:" + closingDate.ToShortDateString() + "\v\v");
                parWireInfo.AppendText("Wiring Instructions:\v");

                Spire.Doc.Documents.Paragraph parBankInfo = section1.AddParagraph();
                parBankInfo.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                parBankInfo.Format.LeftIndent = 80;

                parBankInfo.AppendText("Bank: " + entity.EntityBank + "\v");
                parBankInfo.AppendText("ABA: " + entity.ABA + "\v");
                parBankInfo.AppendText("Account: " + entity.Account + "\v");
                parBankInfo.AppendText("Account Name: " + entity.EntityName + "\v");
                parBankInfo.AppendText("Ref: " + client.ClientName + "\v");
                Spire.Doc.Break pageBreak = new Spire.Doc.Break(document, Spire.Doc.Documents.BreakType.PageBreak);

                Spire.Doc.Document DocOne = new Spire.Doc.Document();

                //DocOne.LoadFromStream(FileUploadControlBailee.PostedFile.InputStream, FileFormat.Docx);
                DocOne.LoadFromFile(@"C:\Users\pdean\OneDrive - Focused Financial Systems\Projects\RAIGroup\Letters\Bailee_PML_RAI Funding.docx", FileFormat.Docx);
                
                DateTime today = DateTime.Today;
                string todayStr;
                todayStr = string.Format("{0:MMMM dd, yyyy}", today);

                DocOne.Replace("TODAYSDATE", todayStr, false, true);

                foreach (Spire.Doc.Section sec in document.Sections)
                {
                    DocOne.Sections.Add(sec.Clone());
                }
                string fileName;
                string todayStrFile = string.Format("{0:yyyy_MM_dd}", today);

                fileName = "C:\\Users\\pdean\\Bailee_" + client.ClientName + "_" + entity.EntityName + "_" + loanNumberList + ".docx";
                //CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                //dialog.InitialDirectory = "C:\\Users";
                //dialog.IsFolderPicker = true;

                //if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                //{
                //    fileName = dialog.FileName + "\\" + fileName;

                //}

                DocOne.SaveToFile(fileName, FileFormat.Docx);

                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception ex) { }

            return RedirectToAction("Index");
        }
        public ActionResult Add()
        {
            var loan = new Loan();

            SetupSelectListItems();
            return View(loan);
        }
        [HttpPost]
        public ActionResult Add(Loan loan)
        {
            ValidateLoan(loan);
            if (ModelState.IsValid)
            {
                //_loanRepository.AddLoan(loan);
                //TempData["Message"] = "Loan Successfully Added";
                return Redirect("Index");
            }
            SetupSelectListItems();
            return View(loan);
        }
        public void ValidateLoan(Loan loan)
        {
            if (ModelState.IsValidField("LoanMortgageAmount") && loan.LoanMortgageAmount <= 0)
            {
                ModelState.AddModelError("LoanMortgageAmount", "The mortgage amount must be greater than 0");
            }
            
        }
        private void SetupClientsSelectListItems()
        {
            ViewBag.ClientsSelectListItems = _loanRepository.GetClients();

        }
        private void SetupEntitiesSelectListItems()
        {
            ViewBag.EntitiesSelectListItems = _loanRepository.GetEntities();

        }
        private void SetupStatesSelectListItems()
        {
            ViewBag.StatesSelectListItems = _loanRepository.GetStates();

        }
        private void SetupLoanTypesSelectListItems()
        {
            ViewBag.LoanTypeSelectListItems = _loanRepository.GetTypes();

        }
        private void SetupInvestorsSelectListItems()
        {
            ViewBag.InvestorsSelectListItems = _loanRepository.GetInvestors();

        }
        private void SetupUsersSelectListItems()
        {
            ViewBag.UsersSelectListItems = _loanRepository.GetUsers();

        }
        private void SetupStatusSelectListItems()
        {
            ViewBag.StatusSelectListItems = _loanRepository.GetStatus();

        }
        private void SetupSelectListItems()
        {
            SetupClientsSelectListItems();
            SetupEntitiesSelectListItems();
            SetupStatesSelectListItems();
            SetupLoanTypesSelectListItems();
            SetupInvestorsSelectListItems();
            SetupUsersSelectListItems();
            SetupStatusSelectListItems();
        }
    }
}