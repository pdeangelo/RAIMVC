using RAI_MVC.Models;
using RAI_MVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Spire.Doc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace RAI_MVC.Controllers
{
    public class RAIController : Controller
    {
        private UsersRepository _usersRepository = null;
        private ClientsRepository _clientsRepository = null;
        private LoanRepository _loanRepository = null;
        private InvestorRepository _investorRepository = null;
        public RAIController()
        {
            _usersRepository = new UsersRepository();
            _loanRepository = new LoanRepository();
            _clientsRepository = new ClientsRepository();
            _investorRepository = new InvestorRepository();
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

        public ActionResult BaileeReport(string loanlist)
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

        public ActionResult AdvanceReport(string loanlist)
        {
            try
            {
                List<Loan> loans = new List<Loan>();

                //foreach (TableFundingLoan loan in LoansDataGrid.ItemsSource)
                //{
                //    if (loan.IsChecked)
                //    {
                //        //Get a refreshed loan because status might have changed
                //        TableFundingLoan loanNew = new TableFundingLoan(loan.LoanID);
                //        loan.LoanUW = new TableFundingLoanUW(loan.LoanID);
                //        loan.LoanFunding = new TableFundingLoanFunding(loan.LoanID);
                //        loans.Add(loan);
                //        if (loanNew.LoanStatus != "3 - Open")
                //        {
                //            ErrorLabel.Content = "Loan Not in Open Status";
                //            ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);
                //            return;
                //        }
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

                section1.PageSetup.Orientation = Spire.Doc.Documents.PageOrientation.Landscape;
                //Add Paragraph

                Spire.Doc.Documents.Paragraph parSchedule1 = section1.AddParagraph();
                parSchedule1.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                Spire.Doc.Fields.TextRange trSchedule1 = parSchedule1.AppendText(entity.EntityName);
                //trSchedule1.CharacterFormat.UnderlineStyle = Spire.Doc.Documents.UnderlineStyle.Single;

                Spire.Doc.Documents.Paragraph parMort = section1.AddParagraph();
                parMort.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

                parMort.AppendText("Advance Report");

                Spire.Doc.Documents.Paragraph parClient = section1.AddParagraph();
                parClient.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                parClient.AppendText(client.ClientName);

                Spire.Doc.Documents.Paragraph parRemitReport = section1.AddParagraph();
                parRemitReport.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                parRemitReport.AppendText("Advance Date " + DateTime.Today.ToShortDateString() + "\v");

                Spire.Doc.Table tblSch = section1.AddTable(true);
                //
                // Table Logic
                //

                String[] Header = { "Business Name", "Client Name", "Loan Number", "Collateral Description", "Mortgage Amount", "Reserve Amount", "Total Transfer" };

                //Add Cells
                // Number of rows is # of loans + header + footer + line for wire fee
                tblSch.ResetCells(loans.Count() + 3, Header.Length);
                int footerRow;
                int wireRow;

                footerRow = loans.Count() + 2;
                wireRow = loans.Count() + 1;
                //Header Row
                Spire.Doc.TableRow FRow2 = tblSch.Rows[0];
                FRow2.IsHeader = true;

                //Row Height
                FRow2.Height = 30;

                //Wire Fee Row
                Spire.Doc.TableRow FRowWire = tblSch.Rows[wireRow];

                FRowWire.Height = 30;
                Spire.Doc.Documents.Paragraph pWire = FRowWire.Cells[0].AddParagraph();
                Spire.Doc.Fields.TextRange TRWire = pWire.AppendText("Wire Transfer Fee If Applicable");
                pWire.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;

                Spire.Doc.Documents.Paragraph pWireFee = FRowWire.Cells[6].AddParagraph();
                Spire.Doc.Fields.TextRange TRWireFee = pWireFee.AppendText("(25.00)");
                pWireFee.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;

                //Merge first 4 cells
                tblSch.ApplyHorizontalMerge((wireRow), 0, 4);

                Spire.Doc.TableRow FRowFooter = tblSch.Rows[footerRow];

                //Merge first 3 cells
                tblSch.ApplyHorizontalMerge((footerRow), 0, 2);

                Spire.Doc.Documents.Paragraph pFooter = FRowFooter.Cells[0].AddParagraph();
                Spire.Doc.Fields.TextRange TRFooter = pFooter.AppendText("Total Due " + client.ClientName);
                pFooter.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;

                //Row Height
                FRowFooter.Height = 23;

                //Header Format
                //FRow.RowFormat.BackColor = Color.AliceBlue;

                for (int i = 0; i < Header.Length; i++)
                {
                    //Cell Alignment
                    Spire.Doc.Documents.Paragraph p = FRow2.Cells[i].AddParagraph();
                    FRow2.Cells[i].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;

                    p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    //Data Format
                    Spire.Doc.Fields.TextRange TR = p.AppendText(Header[i]);
                    //TR.CharacterFormat.FontName = "Calibri";
                    TR.CharacterFormat.FontSize = 14;
                    //TR.CharacterFormat.TextColor = Color.Teal;                
                    TR.CharacterFormat.Bold = true;

                }

                //Data Row

                double totalMortgage = 0;
                double totalAdvance = -25;  // -25 for wire fee
                double totalReserve = 0;

                int r = 0;
                //for (int r = 0; r < data.Length; r++)
                foreach (Loan rptloan in loans)
                {

                    Spire.Doc.TableRow DataRow = tblSch.Rows[r + 1];

                    //Row Height

                    DataRow.Height = 30;

                    //C Represents Column.

                    //for (int c = 0; c < data[r].Length; c++)                    
                    //{                    
                    //Cell Alignment
                    DataRow.Cells[0].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;

                    DataRow.Cells[1].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[2].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[3].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[4].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[5].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[6].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;



                    //Fill Data in Rows

                    Spire.Doc.Documents.Paragraph pBusiness = DataRow.Cells[0].AddParagraph();
                    Spire.Doc.Fields.TextRange TRBusiness = pBusiness.AppendText(rptloan.LoanMortgageeBusiness);

                    Spire.Doc.Documents.Paragraph pBorrower = DataRow.Cells[1].AddParagraph();
                    Spire.Doc.Fields.TextRange TRBorrower = pBorrower.AppendText(rptloan.LoanMortgagee);

                    Spire.Doc.Documents.Paragraph pLoanNumber = DataRow.Cells[2].AddParagraph();
                    Spire.Doc.Fields.TextRange TRLoanNumber = pLoanNumber.AppendText(rptloan.LoanNumber);

                    Spire.Doc.Documents.Paragraph pAddress = DataRow.Cells[3].AddParagraph();
                    Spire.Doc.Fields.TextRange TRAddress = pAddress.AppendText(rptloan.LoanPropertyAddress);

                    Spire.Doc.Documents.Paragraph pMortgageAmount = DataRow.Cells[4].AddParagraph();
                    Spire.Doc.Fields.TextRange trMortgageAmount = pMortgageAmount.AppendText(FormatNumberCommas2dec(rptloan.LoanMortgageAmount.ToString()));

                    Spire.Doc.Documents.Paragraph pReserveAmount = DataRow.Cells[5].AddParagraph();
                    Spire.Doc.Fields.TextRange trReserveAmount = pReserveAmount.AppendText(FormatNumberCommas2dec(rptloan.LoanReserveAmount.ToString()));

                    Spire.Doc.Documents.Paragraph pAdvanceAmount = DataRow.Cells[6].AddParagraph();
                    Spire.Doc.Fields.TextRange trAdvanceAmount = pAdvanceAmount.AppendText(FormatNumberCommas2dec(rptloan.LoanAdvanceAmount.ToString()));

                    totalMortgage += rptloan.LoanMortgageAmount.Value;
                    totalAdvance += rptloan.LoanAdvanceAmount.Value;
                    totalReserve += rptloan.LoanReserveAmount.Value;

                    //Format Cells

                    pLoanNumber.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pBorrower.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pBusiness.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pAddress.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pMortgageAmount.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                    pReserveAmount.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                    pAdvanceAmount.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                    //TRLoanNumber.CharacterFormat.FontName = "Calibri";
                    //TRBorrower.CharacterFormat.FontName = "Calibri";
                    //TRAddress.CharacterFormat.FontName = "Calibri";

                    TRLoanNumber.CharacterFormat.FontSize = 12;
                    TRBorrower.CharacterFormat.FontSize = 12;
                    TRAddress.CharacterFormat.FontSize = 12;

                    TRAddress.CharacterFormat.FontSize = 12;
                    trMortgageAmount.CharacterFormat.FontSize = 12;
                    trReserveAmount.CharacterFormat.FontSize = 12;
                    trAdvanceAmount.CharacterFormat.FontSize = 12;
                    //TR2.CharacterFormat.TextColor = Color.Brown;

                    //}
                    r++;
                }

                Spire.Doc.TableRow FRowFooterMortgage = tblSch.Rows[footerRow];
                Spire.Doc.Documents.Paragraph pFooterMortgage = FRowFooterMortgage.Cells[4].AddParagraph();
                Spire.Doc.Fields.TextRange TRFooterMortgage = pFooterMortgage.AppendText(FormatNumberCommas2dec(totalMortgage.ToString()));
                pFooterMortgage.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                TRFooterMortgage.CharacterFormat.Bold = true;

                Spire.Doc.TableRow FRowFooterReserve = tblSch.Rows[footerRow];
                Spire.Doc.Documents.Paragraph pFooterReserve = FRowFooterReserve.Cells[5].AddParagraph();
                Spire.Doc.Fields.TextRange TRFooterReserve = pFooterReserve.AppendText(FormatNumberCommas2dec(totalReserve.ToString()));
                pFooterReserve.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                TRFooterReserve.CharacterFormat.Bold = true;

                Spire.Doc.TableRow FRowFooterAdvance = tblSch.Rows[footerRow];
                Spire.Doc.Documents.Paragraph pFooterAdvance = FRowFooterAdvance.Cells[6].AddParagraph();
                Spire.Doc.Fields.TextRange TRFooterAdvance = pFooterAdvance.AppendText(FormatNumberCommas2dec(totalAdvance.ToString()));
                pFooterAdvance.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                TRFooterAdvance.CharacterFormat.Bold = true;

                //
                // End Table Logic
                //

                Spire.Doc.Documents.Paragraph parEscrow = section1.AddParagraph();
                parEscrow.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;

                parEscrow.AppendText("Wire Sent to LaRocca Escrow Account");

                DateTime today = DateTime.Today;
                string todayStr;
                todayStr = string.Format("{0:MMMM dd, yyyy}", today);

                string fileName;
                string todayStrFile = string.Format("{0:yyyy_MM_dd}", today);

                fileName = "Advance_" + client.ClientName + "_" + entity.EntityName + "_" + todayStrFile + ".docx";
                //CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                //dialog.InitialDirectory = "C:\\Users";
                //dialog.IsFolderPicker = true;

                //if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                //{
                //    fileName = dialog.FileName + "\\" + fileName;

                //}
                fileName = "C:\\Users\\pdean\\Bailee_" + client.ClientName + "_" + entity.EntityName + "_" + loanlist + ".docx";

                document.SaveToFile(fileName, FileFormat.Docx);

                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception ex)
            {
                //ErrorLabel.Content = ex.Message;
                //ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemittanceReport(string loanlist)
        {
            try
            {
                List<Loan> loans = new List<Loan>();

                //foreach (TableFundingLoan loan in LoansDataGrid.ItemsSource)
                //{
                //    if (loan.IsChecked)
                //    {
                //        loan.LoanUW = new TableFundingLoanUW(loan.LoanID);
                //        loan.LoanFunding = new TableFundingLoanFunding(loan.LoanID);
                //        loan.LoanFees = new TableFundingFees(loan.LoanID);
                //        if (loan.LoanStatus != "4 - Completed")
                //        {
                //            ErrorLabel.Content = "Loan Not in Completed Status";
                //            ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);
                //            return;
                //        }
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


                if (loans[0].BaileeLetterDate == null)
                {
                    //ErrorLabel.Content = "Bailee Letter Date Missing";
                    //ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);
                    //return;
                }
                DateTime baileeLetterDate = (DateTime)loans[0].BaileeLetterDate;

                string baileeLetterDateStr = string.Format("{0:MMMM dd, yyyy}", baileeLetterDate);
                Spire.Doc.Document document = new Spire.Doc.Document();
                //Add Section

                Spire.Doc.Section section1 = document.AddSection();

                section1.PageSetup.Orientation = Spire.Doc.Documents.PageOrientation.Landscape;
                //Add Paragraph

                Spire.Doc.Documents.Paragraph parSchedule1 = section1.AddParagraph();
                parSchedule1.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                Spire.Doc.Fields.TextRange trSchedule1 = parSchedule1.AppendText(entity.EntityName);
                //trSchedule1.CharacterFormat.UnderlineStyle = Spire.Doc.Documents.UnderlineStyle.Single;

                Spire.Doc.Documents.Paragraph parMort = section1.AddParagraph();
                parMort.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

                parMort.AppendText("Remittance Report");

                Spire.Doc.Documents.Paragraph parClient = section1.AddParagraph();
                parClient.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                parClient.AppendText(client.ClientName);

                Spire.Doc.Documents.Paragraph parRemitReport = section1.AddParagraph();
                parRemitReport.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                parRemitReport.AppendText("Remittance Date " + DateTime.Today.ToShortDateString() + "\v");

                Spire.Doc.Table tblSch = section1.AddTable(true);
                //
                // Table Logic
                //

                String[] Header = { "Business Name", "Client Name", "Loan Number", "Interest Percentage", "Mortgage Amount", "Proceeds", "Loan Amount", "Interest", "Origination Fee", "Underwriting/ Administrative Fee", "Total Transfer" };

                //Add Cells
                // Number of rows is # of loans + header + footer + line for wire fee
                tblSch.ResetCells(loans.Count() + 3, Header.Length);
                int footerRow;
                int wireRow;

                footerRow = loans.Count() + 2;
                wireRow = loans.Count() + 1;
                //Header Row
                Spire.Doc.TableRow FRow2 = tblSch.Rows[0];
                FRow2.IsHeader = true;

                //Row Height
                FRow2.Height = 23;

                //Wire Fee Row
                Spire.Doc.TableRow FRowWire = tblSch.Rows[wireRow];

                Spire.Doc.Documents.Paragraph pWire = FRowWire.Cells[0].AddParagraph();
                Spire.Doc.Fields.TextRange TRWire = pWire.AppendText("Wire Transfer Fee If Applicable");
                pWire.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                TRWire.CharacterFormat.FontSize = 12;

                Spire.Doc.Documents.Paragraph pWireFee = FRowWire.Cells[10].AddParagraph();
                Spire.Doc.Fields.TextRange TRWireFee = pWireFee.AppendText("(25.00)");
                pWireFee.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                TRWireFee.CharacterFormat.FontSize = 12;

                //Merge first 4 cells
                tblSch.ApplyHorizontalMerge((wireRow), 0, 8);

                Spire.Doc.TableRow FRowFooter = tblSch.Rows[footerRow];

                //Merge first 3 cells
                tblSch.ApplyHorizontalMerge((footerRow), 0, 2);

                Spire.Doc.Documents.Paragraph pFooter = FRowFooter.Cells[0].AddParagraph();
                Spire.Doc.Fields.TextRange TRFooter = pFooter.AppendText("Total Transfer Due " + client.ClientName);
                pFooter.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
                TRFooter.CharacterFormat.FontSize = 12;

                //Row Height
                FRowFooter.Height = 23;

                //Header Format
                //FRow.RowFormat.BackColor = Color.AliceBlue;

                for (int i = 0; i < Header.Length; i++)
                {
                    //Cell Alignment
                    Spire.Doc.Documents.Paragraph p = FRow2.Cells[i].AddParagraph();
                    FRow2.Cells[i].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;

                    p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    //Data Format
                    Spire.Doc.Fields.TextRange TR = p.AppendText(Header[i]);
                    //TR.CharacterFormat.FontName = "Calibri";
                    TR.CharacterFormat.FontSize = 12;
                    //TR.CharacterFormat.TextColor = Color.Teal;                
                    TR.CharacterFormat.Bold = true;

                }

                //Data Row

                double totalMortgage = 0;
                double totalProceeds = 0;
                double totalLoanAmount = 0;
                double totalInterest = 0;
                double totalOriginationFee = 0;
                double totalUnderwritingFee = 0;
                double totalTransfer = -25; // -25 for wire fee

                int r = 0;
                //for (int r = 0; r < data.Length; r++)
                foreach (Loan rptloan in loans)
                {

                    Spire.Doc.TableRow DataRow = tblSch.Rows[r + 1];

                    //Row Height

                    DataRow.Height = 20;

                    //C Represents Column.

                    //for (int c = 0; c < data[r].Length; c++)                    
                    //{                    
                    //Cell Alignment
                    DataRow.Cells[0].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;

                    DataRow.Cells[1].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[2].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[3].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[4].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[5].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[6].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[7].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[8].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[9].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
                    DataRow.Cells[10].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;



                    //Fill Data in Rows
                    Spire.Doc.Documents.Paragraph pBusiness = DataRow.Cells[0].AddParagraph();
                    Spire.Doc.Fields.TextRange TRBusiness = pBusiness.AppendText(rptloan.LoanMortgageeBusiness);

                    Spire.Doc.Documents.Paragraph pBorrower = DataRow.Cells[1].AddParagraph();
                    Spire.Doc.Fields.TextRange TRBorrower = pBorrower.AppendText(rptloan.LoanMortgagee);

                    Spire.Doc.Documents.Paragraph pLoanNumber = DataRow.Cells[2].AddParagraph();
                    Spire.Doc.Fields.TextRange TRLoanNumber = pLoanNumber.AppendText(rptloan.LoanNumber);

                    Spire.Doc.Documents.Paragraph pAddress = DataRow.Cells[3].AddParagraph();
                    Spire.Doc.Fields.TextRange TRAddress = pAddress.AppendText(FormatPcnt(rptloan.LoanMinimumInterest.ToString()));

                    Spire.Doc.Documents.Paragraph pMortgageAmount = DataRow.Cells[4].AddParagraph();
                    Spire.Doc.Fields.TextRange trMortgageAmount = pMortgageAmount.AppendText(FormatNumberCommas2dec(rptloan.LoanMortgageAmount.ToString()));

                    Spire.Doc.Documents.Paragraph pProceeds = DataRow.Cells[5].AddParagraph();
                    Spire.Doc.Fields.TextRange trProceeds = pProceeds.AppendText(FormatNumberCommas2dec(rptloan.InvestorProceeds.ToString()));

                    Spire.Doc.Documents.Paragraph pLoanAmount = DataRow.Cells[6].AddParagraph();
                    Spire.Doc.Fields.TextRange trLoanAmount = pLoanAmount.AppendText(FormatNumberCommas2dec(rptloan.LoanAdvanceAmount.ToString()));

                    double interestFee = rptloan.InterestFee.Value + loan.CustSvcInterestDiscount.Value;
                    Spire.Doc.Documents.Paragraph pInterest = DataRow.Cells[7].AddParagraph();
                    Spire.Doc.Fields.TextRange trInterest = pInterest.AppendText(FormatNumberCommas2dec(interestFee.ToString()));

                    Spire.Doc.Documents.Paragraph pOrig = DataRow.Cells[8].AddParagraph();
                    double originationFee = loan.OriginationFee.Value + loan.CustSvcOriginationDiscount.Value;
                    Spire.Doc.Fields.TextRange trOrig = pOrig.AppendText(FormatNumberCommas2dec(originationFee.ToString()));

                    double underwritingFee = loan.UnderwritingFee.Value + loan.CustSvcUnderwritingDiscount.Value;
                    Spire.Doc.Documents.Paragraph pUW = DataRow.Cells[9].AddParagraph();
                    Spire.Doc.Fields.TextRange trUW = pUW.AppendText(FormatNumberCommas2dec(underwritingFee.ToString()));

                    Spire.Doc.Documents.Paragraph pTotalTransfer = DataRow.Cells[10].AddParagraph();
                    //Spire.Doc.Fields.TextRange trTotalTransfer = pTotalTransfer.AppendText(FormatNumberCommas2dec(loan.TotalTransfer.ToString()));

                    totalMortgage += loan.LoanMortgageAmount.Value;
                    totalProceeds += loan.InvestorProceeds.Value;
                    totalLoanAmount += loan.LoanAdvanceAmount.Value;
                    totalInterest += loan.InterestFee.Value + loan.CustSvcInterestDiscount.Value;
                    totalOriginationFee += loan.OriginationFee.Value + loan.CustSvcOriginationDiscount.Value;
                    totalUnderwritingFee += loan.UnderwritingFee.Value + loan.CustSvcUnderwritingDiscount.Value;
                    totalTransfer += loan.TotalTransfer.Value;

                    //Format Cells

                    pLoanNumber.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pBorrower.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pAddress.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    pMortgageAmount.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                    pProceeds.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                    pLoanAmount.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                    pInterest.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                    pOrig.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                    pUW.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                    pTotalTransfer.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                    //TRLoanNumber.CharacterFormat.FontName = "Calibri";
                    //TRBorrower.CharacterFormat.FontName = "Calibri";
                    //TRAddress.CharacterFormat.FontName = "Calibri";

                    TRLoanNumber.CharacterFormat.FontSize = 12;
                    TRBorrower.CharacterFormat.FontSize = 12;
                    //TRAddress.CharacterFormat.FontSize = 12;

                    //TRAddress.CharacterFormat.FontSize = 12;
                    trMortgageAmount.CharacterFormat.FontSize = 12;
                    trProceeds.CharacterFormat.FontSize = 12;
                    //trLoanAmount.CharacterFormat.FontSize = 12;
                    trInterest.CharacterFormat.FontSize = 12;
                    trOrig.CharacterFormat.FontSize = 12;
                    trUW.CharacterFormat.FontSize = 12;
                    //trTotalTransfer.CharacterFormat.FontSize = 12;
                    //TR2.CharacterFormat.TextColor = Color.Brown;

                    //}
                    r++;
                }

                Spire.Doc.TableRow FRowFooterMortgage = tblSch.Rows[footerRow];
                Spire.Doc.Documents.Paragraph pFooterMortgage = FRowFooterMortgage.Cells[4].AddParagraph();
                Spire.Doc.Fields.TextRange TRFooterMortgage = pFooterMortgage.AppendText(FormatNumberCommas2dec(totalMortgage.ToString()));
                pFooterMortgage.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                TRFooterMortgage.CharacterFormat.Bold = true;
                TRFooterMortgage.CharacterFormat.FontSize = 12;

                Spire.Doc.TableRow FRowFooterProceeds = tblSch.Rows[footerRow];
                Spire.Doc.Documents.Paragraph pFooterProceeds = FRowFooterProceeds.Cells[5].AddParagraph();
                Spire.Doc.Fields.TextRange TRFooterProceeds = pFooterProceeds.AppendText(FormatNumberCommas2dec(totalProceeds.ToString()));
                pFooterProceeds.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                TRFooterProceeds.CharacterFormat.Bold = true;
                TRFooterProceeds.CharacterFormat.FontSize = 12;

                Spire.Doc.TableRow FRowFooterLoanAmount = tblSch.Rows[footerRow];
                Spire.Doc.Documents.Paragraph pFooterLoanAmount = FRowFooterLoanAmount.Cells[6].AddParagraph();
                Spire.Doc.Fields.TextRange TRFooterLoanAmount = pFooterLoanAmount.AppendText(FormatNumberCommas2dec(totalLoanAmount.ToString()));
                pFooterLoanAmount.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                TRFooterLoanAmount.CharacterFormat.Bold = true;
                TRFooterLoanAmount.CharacterFormat.FontSize = 12;

                Spire.Doc.TableRow FRowFooterInterest = tblSch.Rows[footerRow];
                Spire.Doc.Documents.Paragraph pFooterInterest = FRowFooterInterest.Cells[7].AddParagraph();
                Spire.Doc.Fields.TextRange TRFooterInterest = pFooterInterest.AppendText(FormatNumberCommas2dec(totalInterest.ToString()));
                pFooterInterest.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                TRFooterInterest.CharacterFormat.Bold = true;
                TRFooterInterest.CharacterFormat.FontSize = 12;

                Spire.Doc.TableRow FRowFooterOrig = tblSch.Rows[footerRow];
                Spire.Doc.Documents.Paragraph pFooterOrig = FRowFooterOrig.Cells[8].AddParagraph();
                Spire.Doc.Fields.TextRange TRFooterOrig = pFooterOrig.AppendText(FormatNumberCommas2dec(totalOriginationFee.ToString()));
                pFooterOrig.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                TRFooterOrig.CharacterFormat.Bold = true;
                TRFooterOrig.CharacterFormat.FontSize = 12;

                Spire.Doc.TableRow FRowFooterUW = tblSch.Rows[footerRow];
                Spire.Doc.Documents.Paragraph pFooterUW = FRowFooterUW.Cells[9].AddParagraph();
                Spire.Doc.Fields.TextRange TRFooterUW = pFooterUW.AppendText(FormatNumberCommas2dec(totalUnderwritingFee.ToString()));
                pFooterUW.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                TRFooterUW.CharacterFormat.Bold = true;
                TRFooterUW.CharacterFormat.FontSize = 12;

                Spire.Doc.TableRow FRowFooterTotalTransfer = tblSch.Rows[footerRow];
                Spire.Doc.Documents.Paragraph pFooterTotalTransfer = FRowFooterTotalTransfer.Cells[10].AddParagraph();
                Spire.Doc.Fields.TextRange TRFooterTotalTransfer = pFooterTotalTransfer.AppendText(FormatNumberCommas2dec(totalTransfer.ToString()));
                pFooterTotalTransfer.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
                TRFooterTotalTransfer.CharacterFormat.Bold = true;
                TRFooterTotalTransfer.CharacterFormat.FontSize = 12;

                //
                // End Table Logic
                //

                Spire.Doc.Documents.Paragraph parEscrow = section1.AddParagraph();
                parEscrow.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;

                parEscrow.AppendText("Wire Sent to " + client.ClientName);

                //document.SaveToFile("C:/Temp/result.docx", FileFormat.Docx);

                //Spire.Doc.Document DocOne = new Spire.Doc.Document();

                //DocOne.LoadFromFile("C:/Temp/Release Text.docx", FileFormat.Docx);

                //DocOne.Replace("TODAYSDATE", todayStr, false, true);

                //DocOne.Replace("BAILEELETTERDATE", baileeLetterDateStr, false, true);

                //foreach (Spire.Doc.Section sec in document.Sections)
                //{
                //    DocOne.Sections.Add(sec.Clone());
                //}
                DateTime today = DateTime.Today;
                string todayStr;
                todayStr = string.Format("{0:MMMM dd, yyyy}", today);

                string fileName;
                string todayStrFile = string.Format("{0:yyyy_MM_dd}", today);

                fileName = "Remittance_" + client.ClientName + "_" + entity.EntityName + "_" + todayStrFile + ".docx";
                //CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                //dialog.InitialDirectory = "C:\\Users";
                //dialog.IsFolderPicker = true;

                //if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                //{
                //    fileName = dialog.FileName + "\\" + fileName;

                //}


                document.SaveToFile(fileName, FileFormat.Docx);

                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception ex)
            {
                //ErrorLabel.Content = ex.Message;
                //ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);
            }
            return RedirectToAction("Index");
        }
       public ActionResult ReleaseReport(string loanlist)
        {
            try
            {
                List<Loan> loans = new List<Loan>();

                //foreach (TableFundingLoan loan in LoansDataGrid.ItemsSource)
                //{
                //    if (loan.IsChecked)
                //    {
                //        loan.LoanUW = new TableFundingLoanUW(loan.LoanID);
                //        loan.LoanFunding = new TableFundingLoanFunding(loan.LoanID);
                //        loan.LoanFees = new TableFundingFees(loan.LoanID);
                //        if (loan.LoanStatus != "4 - Completed")
                //        {
                //            ErrorLabel.Content = "Loan Not in Completed Status";
                //            ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);
                //            return;
                //        }
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



                if (loans[0].BaileeLetterDate == null)
                {
                    //ErrorLabel.Content = "Bailee Letter Date Missing";
                    //ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);
                    //return;
                }

                DateTime baileeLetterDate = (DateTime)loans[0].BaileeLetterDate;

                string baileeLetterDateStr = string.Format("{0:MMMM dd, yyyy}", baileeLetterDate);
                Spire.Doc.Document document = new Spire.Doc.Document();
                //Add Section

                Spire.Doc.Section section1 = document.AddSection();
                section1.PageSetup.Orientation = Spire.Doc.Documents.PageOrientation.Portrait;

                //Add Paragraph

                Spire.Doc.Documents.Paragraph parSchedule1 = section1.AddParagraph();
                parSchedule1.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                Spire.Doc.Fields.TextRange trSchedule1 = parSchedule1.AppendText("Schedule 1\v");
                trSchedule1.CharacterFormat.UnderlineStyle = Spire.Doc.Documents.UnderlineStyle.Single;

                Spire.Doc.Documents.Paragraph parMort = section1.AddParagraph();
                parMort.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

                parMort.AppendText("Mortgage Loan Schedule\v");

                Spire.Doc.Table tblSch = section1.AddTable(true);
                //
                // Table Logic
                //

                String[] Header = { "Loan ID", "Borrower", "Address" };

                //Add Cells
                tblSch.ResetCells(loans.Count() + 1, Header.Length);
                //Header Row
                Spire.Doc.TableRow FRow2 = tblSch.Rows[0];
                FRow2.IsHeader = true;

                //Row Height
                FRow2.Height = 23;

                //Header Format
                //FRow.RowFormat.BackColor = Color.AliceBlue;

                for (int i = 0; i < Header.Length; i++)
                {
                    //Cell Alignment
                    Spire.Doc.Documents.Paragraph p = FRow2.Cells[i].AddParagraph();
                    FRow2.Cells[i].CellFormat.VerticalAlignment = Spire.Doc.Documents.VerticalAlignment.Middle;
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
                foreach (Loan rptLoan in loans)
                {

                    Spire.Doc.TableRow DataRow = tblSch.Rows[r + 1];

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
                    loanNumberList += loan.LoanNumber + " ";

                    Spire.Doc.Documents.Paragraph pLoanNumber = DataRow.Cells[0].AddParagraph();
                    Spire.Doc.Fields.TextRange TRLoanNumber = pLoanNumber.AppendText(rptLoan.LoanNumber);

                    Spire.Doc.Documents.Paragraph pBorrower = DataRow.Cells[1].AddParagraph();
                    Spire.Doc.Fields.TextRange TRBorrower = pBorrower.AppendText(rptLoan.LoanMortgagee);

                    Spire.Doc.Documents.Paragraph pAddress = DataRow.Cells[2].AddParagraph();
                    Spire.Doc.Fields.TextRange TRAddress = pAddress.AppendText(rptLoan.LoanPropertyAddress);

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

                //document.SaveToFile(MyVariables.letterFilePath + "result.docx", FileFormat.Docx);

                Spire.Doc.Document DocOne = new Spire.Doc.Document();

                //OpenFileDialog fileDialog = new OpenFileDialog();
                //string releaseFileName = "";
                ////releaseFileName = "Release_" + client.ClientName + "_" + entity.EntityName + ".docx";

                //if (fileDialog.ShowDialog() == true)
                //{
                //    releaseFileName = fileDialog.FileName;
                //}

                //DocOne.LoadFromFile(releaseFileName, FileFormat.Docx);
                DocOne.LoadFromFile(@"C:\Users\pdean\OneDrive - Focused Financial Systems\Projects\RAIGroup\Letters\Bailee_PML_RAI Funding.docx", FileFormat.Docx);

                DateTime today = DateTime.Today;
                string todayStr;
                todayStr = string.Format("{0:MMMM dd, yyyy}", today);

                DocOne.Replace("TODAYSDATE", todayStr, false, true);

                DocOne.Replace("BAILEELETTERDATE", baileeLetterDateStr, false, true);

                foreach (Spire.Doc.Section sec in document.Sections)
                {
                    DocOne.Sections.Add(sec.Clone());
                }
                string fileName;
                string todayStrFile = string.Format("{0:yyyy_MM_dd}", today);

                fileName = "Release_" + client.ClientName + "_" + entity.EntityName + "_" + loanNumberList + ".docx";


                //CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                //dialog.InitialDirectory = "C:\\Users";
                //dialog.IsFolderPicker = true;

                //if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                //{
                //    fileName = dialog.FileName + "\\" + fileName;

                //}
                fileName = "C:\\Users\\pdean\\Release_" + client.ClientName + "_" + entity.EntityName + "_" + loanlist + ".docx";

                DocOne.SaveToFile(fileName, FileFormat.Docx);

                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception ex)
            {
                //ErrorLabel.Content = ex.Message;
                //ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);
            }


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
        public ActionResult Investors()
        {
            var investors = _investorRepository.GetInvestors();
            return View(investors);
        }
        public ActionResult EditInvestor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Investor investor = _investorRepository.GetInvestor((int)id);
            if (investor == null)
            {
                return HttpNotFound();
            }
            SetupSelectListItems();
            return View(investor);
        }
        [HttpPost]
        public ActionResult EditInvestor(Investor investor)
        {
            if (ModelState.IsValid)
            {
                _investorRepository.UpdateInvestor(investor);

                TempData["Message"] = "Investor Successfully Updated";
                return RedirectToAction("Investors");
            }

            return View(investor);
        }
        public ActionResult DeleteInvestor(int id)
        {
            _investorRepository.DeleteInvestor(id);

            TempData["Message"] = "Investor Successfully Deleted";
            return RedirectToAction("Investors");
        }
        public ActionResult Clients()
        {
            var clients = _clientsRepository.GetClients();
            return View(clients);
        }
        public ActionResult EditClient(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = _clientsRepository.GetClient((int)id);
            if (client == null)
            {
                return HttpNotFound();
            }
            SetupSelectListItems();
            return View(client);
        }
        [HttpPost]
        public ActionResult EditClient(Client client)
        {
            if (ModelState.IsValid)
            {
                _clientsRepository.UpdateClient(client);

                TempData["Message"] = "Client Successfully Updated";
                return RedirectToAction("Clients");
            }
            
            return View(client);
        }
        public ActionResult DeleteClient(int id)
        {
            _clientsRepository.DeleteClient(id);

            TempData["Message"] = "Client Successfully Deleted";
            return RedirectToAction("Clients");
        }

        public ActionResult Users()
        {
            var clients = _usersRepository.GetUsers();
            return View(clients);
        }

        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = _usersRepository.GetUser((int)id);
            if (user == null)
            {
                return HttpNotFound();
            }
            SetupRoleSelectListItems();
            return View(user);
        }
        [HttpPost]
        public ActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                _usersRepository.UpdateUser(user);

                TempData["Message"] = "User Successfully Updated";
                return RedirectToAction("Users");
            }

            return View(user);
        }
        public ActionResult DeleteUser(int id)
        {
            _usersRepository.DeleteUser(id);

            TempData["Message"] = "User Successfully Deleted";
            return RedirectToAction("Users");
        }
        //

        public ActionResult Roles()
        {
            var clients = _usersRepository.GetRoles();
            return View(clients);
        }

        public ActionResult EditRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Role role = _usersRepository.GetRole((int)id);
            if (role == null)
            {
                return HttpNotFound();
            }
            SetupRoleSelectListItems();
            return View(role);
        }
        [HttpPost]
        public ActionResult EditRole(Role role)
        {
            if (ModelState.IsValid)
            {
                _usersRepository.UpdateRole(role);

                TempData["Message"] = "Role Successfully Updated";
                return RedirectToAction("Roles");
            }

            return View(role);
        }
        public ActionResult DeleteRole(int id)
        {
            _usersRepository.DeleteRole(id);

            TempData["Message"] = "Role Successfully Deleted";
            return RedirectToAction("Roles");
        }

        public ActionResult SalesReport()
        {
            var fromToDate = new FromToDate();
            return View(fromToDate);
        }

      
        [HttpPost]
        public JsonResult RunSalesReport(DateTime? dtFromDate, DateTime? dtToDate)
        {
            try
            {

                DateTime fromDate = new DateTime();
                DateTime toDate = new DateTime();

                //if (!dtFromDate.SelectedDate.HasValue)
                //{
                //    ErrorLabel.Content = "Please enter a valid date";
                //    return;
                //}
                //else
                //    fromDate = dtFromDate.SelectedDate.Value;
                //if (!dtToDate.SelectedDate.HasValue)
                //{
                //    ErrorLabel.Content = "Please enter a valid date";
                //    return;
                //}
                //else
                //    toDate = dtToDate.SelectedDate.Value;
                //fromDate = dtFromDate.Value;
                //toDate = dtToDate.Value;
                var rptData = _loanRepository.GetLoans();

                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create("C:\\Users\\pdean\\SalesReport.xlsx", SpreadsheetDocumentType.Workbook))
                {
                    WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();

                    workbookPart.Workbook = new Workbook();
                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    SheetData sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                    Sheet sheet = new Sheet()
                    {
                        Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                        SheetId = 1,
                        Name = "Sheet1"
                    };

                    #region Headers
                    Row headerRow = new Row() { RowIndex = 1 };
                    Cell header = new Cell() { CellReference = "A1", CellValue = new CellValue("ClientName"), DataType = CellValues.String };
                    headerRow.Append(header);

                    Cell header1 = new Cell() { CellReference = "B1", CellValue = new CellValue("Entity"), DataType = CellValues.String };
                    headerRow.Append(header1);

                    Cell header2 = new Cell() { CellReference = "C1", CellValue = new CellValue("Investor"), DataType = CellValues.String };
                    headerRow.Append(header2);
                    Cell header3 = new Cell() { CellReference = "D1", CellValue = new CellValue("LoanNumber"), DataType = CellValues.String };
                    headerRow.Append(header3);
                    Cell header4 = new Cell() { CellReference = "E1", CellValue = new CellValue("Customer Name"), DataType = CellValues.String };
                    headerRow.Append(header4);
                    Cell header5 = new Cell() { CellReference = "F1", CellValue = new CellValue("Gross Amount"), DataType = CellValues.String };
                    headerRow.Append(header5);
                    Cell header6 = new Cell() { CellReference = "G1", CellValue = new CellValue("Advance"), DataType = CellValues.String };
                    headerRow.Append(header6);
                    Cell header7 = new Cell() { CellReference = "H1", CellValue = new CellValue("State"), DataType = CellValues.String };
                    headerRow.Append(header7);
                    Cell header8 = new Cell() { CellReference = "I1", CellValue = new CellValue("LoanDwellingType"), DataType = CellValues.String };
                    headerRow.Append(header8);
                    Cell header9 = new Cell() { CellReference = "J1", CellValue = new CellValue("DateDepositedInEscrow"), DataType = CellValues.String };
                    headerRow.Append(header9);
                    Cell header10 = new Cell() { CellReference = "K1", CellValue = new CellValue("LoanType"), DataType = CellValues.String };
                    headerRow.Append(header10);
                    Cell header11 = new Cell() { CellReference = "L1", CellValue = new CellValue("WeekToDate"), DataType = CellValues.String };
                    headerRow.Append(header11);
                    Cell header12 = new Cell() { CellReference = "M1", CellValue = new CellValue("MonthToDate"), DataType = CellValues.String };
                    headerRow.Append(header12);
                    Cell header13 = new Cell() { CellReference = "N1", CellValue = new CellValue("RowType"), DataType = CellValues.String };
                    headerRow.Append(header13);
                    Cell header14 = new Cell() { CellReference = "O1", CellValue = new CellValue("SortOrder"), DataType = CellValues.String };
                    headerRow.Append(header14);
                    Cell header15 = new Cell() { CellReference = "P1", CellValue = new CellValue("SortField"), DataType = CellValues.String };
                    headerRow.Append(header15);
                    sheetData.Append(headerRow);
                    #endregion

                    int col = 1;
                    UInt32Value row = 2;
                    foreach (var loan in rptData)
                    {
                        Row lastRow = sheetData.Elements<Row>().LastOrDefault();
                        string cellRef = GetExcelColumnName(col++) + row;
                        Row detailRow = new Row() { RowIndex = row };
                        Cell detail = new Cell() { CellReference = cellRef, CellValue = new CellValue(loan.Client.ClientName), DataType = CellValues.String };
                        detailRow.Append(detail);

                        string cellRef2 = GetExcelColumnName(col++) + row;
                        Row detailRow2 = new Row() { RowIndex = row };
                        Cell detail2 = new Cell() { CellReference = cellRef2, CellValue = new CellValue(loan.Entity.EntityName), DataType = CellValues.String };
                        detailRow.Append(detail2);

                        string cellRef3 = GetExcelColumnName(col++) + row;
                        Row detailRow3 = new Row() { RowIndex = row };
                        Cell detail3 = new Cell() { CellReference = cellRef3, CellValue = new CellValue(loan.Investor.InvestorName), DataType = CellValues.String };
                        detailRow.Append(detail3);


                        string cellRef4 = GetExcelColumnName(col++) + row;
                        Row detailRow4 = new Row() { RowIndex = row };
                        Cell detail4 = new Cell() { CellReference = cellRef4, CellValue = new CellValue(loan.LoanNumber), DataType = CellValues.String };
                        detailRow.Append(detail4);

                        string cellRef5 = GetExcelColumnName(col++) + row;
                        Row detailRow5 = new Row() { RowIndex = row };
                        Cell detail5 = new Cell() { CellReference = cellRef5, CellValue = new CellValue(loan.LoanMortgagee), DataType = CellValues.String };
                        detailRow.Append(detail5);

                        string cellRef6 = GetExcelColumnName(col++) + row;
                        Row detailRow6 = new Row() { RowIndex = row };
                        Cell detail6 = new Cell() { CellReference = cellRef6, CellValue = new CellValue(loan.LoanMortgageAmount.Value), DataType = CellValues.Number };
                        detailRow.Append(detail6);

                        string cellRef7 = GetExcelColumnName(col++) + row;
                        Row detailRow7 = new Row() { RowIndex = row };
                        Cell detail7 = new Cell() { CellReference = cellRef7, CellValue = new CellValue(loan.LoanAdvanceAmount.Value), DataType = CellValues.Number };
                        detailRow.Append(detail7);

                        string cellRef8 = GetExcelColumnName(col++) + row;
                        Row detailRow8 = new Row() { RowIndex = row };
                        Cell detail8 = new Cell() { CellReference = cellRef8, CellValue = new CellValue(loan.State.State1), DataType = CellValues.String };
                        detailRow.Append(detail8);

                        string cellRef9 = GetExcelColumnName(col++) + row;
                        Row detailRow9 = new Row() { RowIndex = row };
                        Cell detail9 = new Cell() { CellReference = cellRef9, CellValue = new CellValue(loan.DwellingType.DwellingType1), DataType = CellValues.String };
                        detailRow.Append(detail9);

                        string cellRef10 = GetExcelColumnName(col++) + row;
                        if (loan.DateDepositedInEscrow != null)
                        {
                            Row detailRow10 = new Row() { RowIndex = row };
                            Cell detail10 = new Cell() { CellReference = cellRef10, CellValue = new CellValue(loan.DateDepositedInEscrow.Value), DataType = CellValues.Date };
                            detailRow.Append(detail10);
                        }
                    

                        string cellRef11 = GetExcelColumnName(col++) + row;
                        Row detailRow11 = new Row() { RowIndex = row };
                        Cell detail11 = new Cell() { CellReference = cellRef11, CellValue = new CellValue(loan.LoanType.LoanTypeName), DataType = CellValues.String };
                        detailRow.Append(detail11);

                        //string cellRef12 = GetExcelColumnName(col++) + row;
                        //Row detailRow12 = new Row() { RowIndex = row };
                        //Cell detail12 = new Cell() { CellReference = cellRef12, CellValue = new CellValue(loan.Investor.InvestorName), DataType = CellValues.String };
                        //detailRow.Append(detail12);

                        //string cellRef13 = GetExcelColumnName(col++) + row;
                        //Row detailRow13 = new Row() { RowIndex = row };
                        //Cell detail13 = new Cell() { CellReference = cellRef13, CellValue = new CellValue(loan.Investor.InvestorName), DataType = CellValues.String };
                        //detailRow.Append(detail13);

                        //string cellRef14 = GetExcelColumnName(col++) + row;
                        //Row detailRow14 = new Row() { RowIndex = row };
                        //Cell detail14 = new Cell() { CellReference = cellRef14, CellValue = new CellValue(loan.Investor.InvestorName), DataType = CellValues.String };
                        //detailRow.Append(detail14);

                        //string cellRef15 = GetExcelColumnName(col++) + row;
                        //Row detailRow15 = new Row() { RowIndex = row };
                        //Cell detail15 = new Cell() { CellReference = cellRef15, CellValue = new CellValue(loan.Investor.InvestorName), DataType = CellValues.String };
                        //detailRow.Append(detail15);

                        sheetData.InsertAfter(detailRow, lastRow);

                    }
                    sheets.Append(sheet);
                    workbookPart.Workbook.Save();

                }
                System.Diagnostics.Process.Start("C:\\Users\\pdean\\SalesReport.xlsx");
                //DataSet report = RunsStoredProc.RunStoredProc("TableFunding_SalesReport", "FromDate", fromDate.ToString(), "ToDate", toDate.ToString(), "", "", "", "", "", "", "", "", "", 0);
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                ////tableFunding.Delete();
                //using (ExcelPackage pck = new ExcelPackage())
                //{
                //    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sales Report");

                //    ws.Cells["A4"].LoadFromDataTable(report.Tables[0], true);
                //    ws.Cells["A2"].Value = "Sales Report";
                //    ws.Cells["A3"].Formula = "=Today()";

                //    ws.Cells["A3"].Style.Numberformat.Format = "yyyy-mm-dd";

                //    ws.Cells["F:G"].Style.Numberformat.Format = "#,##0.00";

                //    ws.Cells["H:H"].Style.Numberformat.Format = "yyyy-mm-dd";

                //    ws.Cells["A2"].Style.Font.Size = 18;
                //    ws.Cells["A3"].Style.Font.Size = 18;

                //    ws.Cells[4, 1, 4, 38].Style.Font.Size = 14;


                //    ws.Cells[4, 1, 4, 38].Style.Fill.PatternType = ExcelFillStyle.Solid;

                //    ws.Cells[4, 1, 4, 38].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkGray);

                //    ws.Cells[4, 1, 4, 38].Style.Font.Color.SetColor(System.Drawing.Color.White);


                //    string client = "";
                //    string investor = "";
                //    int clientStartRow = 0;
                //    int clientInvestorStartRow = 0;
                //    string rowType = "";
                //    //double totalOpenMortgageBalance = 0;
                //    //double totalOpenAdvanceBalance = 0;
                //    int dataStartRow = 5;
                //    int dataEndRow = 0;
                //    for (int row = dataStartRow; row <= 1000; row++)
                //    {
                //        if (ws.Cells[row, 1].Value == null)
                //        {
                //            break;
                //        }

                //        if (row == dataStartRow)
                //        {
                //            client = ws.Cells[row, 1].Text;
                //            investor = ws.Cells[row, 3].Text;
                //            clientStartRow = row;
                //            clientInvestorStartRow = row;
                //        }

                //        rowType = ws.Cells[row, 14].Text;
                //        if (rowType == "Grand Total")
                //        {
                //            ws.Cells[row, 4].Value = "";
                //            ws.Cells[row, 6].Formula = "=SUBTOTAL(9,F" + dataStartRow + ":F" + (row - 1) + ")";
                //            ws.Cells[row, 7].Formula = "=SUBTOTAL(9,G" + dataStartRow + ":G" + (row - 1) + ")";

                //            ws.Cells[row, 8].Value = "";
                //            ws.Cells[row, 1, row, 11].Style.Font.Bold = true;
                //            ws.Cells[row, 1, row, 11].Style.Font.Size = 14;

                //            ws.Cells[row, 1, row, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;

                //            ws.Cells[row, 1, row, 11].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkGray);

                //            ws.Cells[row, 1, row, 11].Style.Font.Color.SetColor(System.Drawing.Color.White);

                //        }

                //        if (rowType == "Client Total")
                //        {
                //            ws.Cells[row, 4].Value = "";
                //            ws.Cells[row, 6].Formula = "=SUBTOTAL(9,F" + clientStartRow + ":F" + (row - 1) + ")";
                //            ws.Cells[row, 7].Formula = "=SUBTOTAL(9,G" + clientStartRow + ":G" + (row - 1) + ")";

                //            ws.Cells[row, 8].Value = "";
                //            ws.Cells[row, 9].Value = "";
                //            ws.Cells[row, 10].Value = "";
                //            ws.Cells[row, 11].Value = "";
                //            ws.Cells[row, 1, row, 11].Style.Font.Bold = true;
                //            ws.Cells[row, 1, row, 11].Style.Font.Size = 12;

                //            ws.Cells[row, 1, row, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;

                //            ws.Cells[row, 1, row, 11].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                //        }

                //        if (rowType == "Client Investor Total")
                //        {
                //            ws.Cells[row, 4].Value = "";
                //            ws.Cells[row, 6].Formula = "=SUBTOTAL(9,F" + clientInvestorStartRow + ":F" + (row - 1) + ")";
                //            ws.Cells[row, 7].Formula = "=SUBTOTAL(9,G" + clientInvestorStartRow + ":G" + (row - 1) + ")";

                //            ws.Cells[row, 8].Value = "";
                //            ws.Cells[row, 9].Value = "";
                //            ws.Cells[row, 10].Value = "";
                //            ws.Cells[row, 11].Value = "";
                //            ws.Cells[row, 1, row, 11].Style.Font.Bold = true;
                //            ws.Cells[row, 1, row, 11].Style.Font.Size = 13;

                //            ws.Cells[row, 1, row, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;

                //            ws.Cells[row, 1, row, 11].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gray);
                //        }
                //        if (rowType == "Detail")
                //        {


                //        }
                //        if (ws.Cells[row, 1].Text != client && rowType == "Detail")
                //        {
                //            investor = "";
                //            client = ws.Cells[row, 1].Text;
                //            clientStartRow = row;
                //        }
                //        if (ws.Cells[row, 3].Text != investor && rowType == "Detail")
                //        {
                //            investor = ws.Cells[row, 3].Text;
                //            clientInvestorStartRow = row;
                //        }

                //        dataEndRow = row;
                //    }

                //    ExcelRange dataRange = ws.Cells[dataStartRow - 1, 1, dataEndRow, 10];
                //    dataRange.AutoFilter = true;

                //    ws.Cells["A:P"].AutoFitColumns();
                //    ws.Column(6).Width = 20;
                //    ws.Column(7).Width = 20;

                //    CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                //    dialog.InitialDirectory = "C:\\Users";
                //    dialog.IsFolderPicker = true;

                //    string fileName;

                //    fileName = "";

                //    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                //    {
                //        fileName = dialog.FileName + "\\" + "SalesReport.xlsx";

                //    }



                //    //System.IO.FileInfo tableFunding = new System.IO.FileInfo(MyVariables.excelReportFilePath + "SalesReport.xlsx");

                //    System.IO.FileInfo tableFunding = new System.IO.FileInfo(fileName);


                //    tableFunding.Delete();

                //    pck.SaveAs(tableFunding);
                //    System.Diagnostics.Process.Start(fileName);
                //}
            }
            catch (Exception ex)
            {
                //ErrorLabel.Content = ex.Message;
                //ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);
            }
            return Json("");
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
        private void SetupRoleSelectListItems()
        {
            ViewBag.RoleSelectListItems = _loanRepository.GetRoles();

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
        public string FormatNumberCommas(string input)
        {
            double no = double.Parse(input.ToString());
            return no.ToString("#,##0");
        }
        public string FormatNumberCommas2dec(string input)
        {
            double no = double.Parse(input.ToString());
            return no.ToString("#,##0.00;(#,##0.00)");
        }

        public string FormatPcnt(string input)
        {
            double no = double.Parse(input.ToString());
            return no.ToString("0.000%");
        }
        public string GetExcelColumnName(int columnNumber)
        {
            string columnName = String.Empty;
            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;

            }
            return columnName;
        }
    }
}