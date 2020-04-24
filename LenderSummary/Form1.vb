Imports System.Configuration
Imports System.Collections.Specialized
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class Form1

    Public Class Extract
        Property AccountID As String
        Property LoanID As String
        Property LoanName As String
        Property LHID As String
        Property OrderID As String
        Property TransactionDate As String
        Property TransType As Integer
        Property TransDesc As String
        Property Amount As Integer
        Property LHAmtIn As String
        Property LHAmtOut As String
        Property LHBal As Integer
        Property PIBal As Integer


    End Class

    Public dsLenders As DataSet
    Public dsLoans As DataSet
    Public dsOrders As DataSet
    Public dsTrans As DataSet
    Public dsHoldings As DataSet
    Public ExtractName As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        frmAccountName.Text = ""
        frmLoanName.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Go.Click
        Dim MySQL, strConn, sHTML, bHTML, sUsers As String
        Dim MyConn As FirebirdSql.Data.FirebirdClient.FbConnection
        Dim Cmd As FirebirdSql.Data.FirebirdClient.FbCommand
        Dim Adaptor As FirebirdSql.Data.FirebirdClient.FbDataAdapter
        Dim dr1, dr2, dr3 As DataRow
        Dim environ As String = "L"
        Dim connection As String = "FBConnectionString"
        Dim iAccountid, iLoanID, iLHID, tLoanID As Integer
        Dim iParentLid As Integer = Nothing
        Dim xBusinessName As String = Nothing
        Dim xParentBusName As String = Nothing
        Dim ExtractList As New List(Of Extract)
        Dim PrevAccountid As Integer = Nothing
        Dim dispLoanID, dispLoanName, dispLHID, disporderid, dispDate, dispAccountID As String
        Dim printedLoanid, printedOrderID, printedLHID, printedAccountID As Integer
        Dim SomethingSupplied As Integer = Nothing
        Dim sqlAccount As String = ""
        Dim sqlAccount2 As String = ""
        Dim sqlAccount3 As String = ""
        Dim sqlLoan As String = ""
        Dim sqlLHID As String = ""


        sqlAccount = ""
        sqlAccount2 = ""
        sqlAccount3 = ""
        If IsNumeric(frmAccountID.Text) Then
            iAccountid = frmAccountID.Text
            strConn = ConfigurationManager.ConnectionStrings(connection).ConnectionString
            MyConn = New FirebirdSql.Data.FirebirdClient.FbConnection(strConn)
            MyConn.Open()

            MySQL = "select a.accountid, trim(u.firstname) as firstname, trim (u.lastname) as lastname, trim (u.companyname) as companyname
                  from accounts a , users u
                  where a.accountid = " & iAccountid &
                  " and a.userid = u.userid "

            dsLenders = New DataSet
            Adaptor = New FirebirdSql.Data.FirebirdClient.FbDataAdapter(MySQL, MyConn)

            Adaptor.Fill(dsLenders)

            MyConn.Close()

            Dim iLenderCounter As Integer = dsLenders.Tables(0).Rows.Count

            For i = 0 To iLenderCounter - 1
                dr1 = dsLenders.Tables(0).Rows(i)

                xBusinessName = dr1("firstname") & " " & dr1("lastname") & " - " & dr1("companyname")

                frmAccountName.Text = xBusinessName
                sqlAccount = " and o.accountid =  " & iAccountid
                sqlAccount2 = " and f.accountid =  " & iAccountid
                sqlAccount3 = " and b.accountid =  " & iAccountid
                SomethingSupplied = 1
            Next
        End If

        If frmLoanExtr.Text <> "" And IsNumeric(frmLoanID.Text) Then
            frmLoanExtr.Text = ""
        End If


        sqlLoan = ""
        If frmLoanExtr.Text <> "" Then
            Dim xLoanExtr As String = "'%" & frmLoanExtr.Text & "%'"
            xLoanExtr = xLoanExtr.ToLower
            strConn = ConfigurationManager.ConnectionStrings(connection).ConnectionString
            MyConn = New FirebirdSql.Data.FirebirdClient.FbConnection(strConn)
            MyConn.Open()

            MySQL = "select l.loanid, trim(l.business_name) as business_name
                  from loans l
                  where lower(l.business_name) like " & xLoanExtr

            dsLoans = New DataSet
            Adaptor = New FirebirdSql.Data.FirebirdClient.FbDataAdapter(MySQL, MyConn)

            Adaptor.Fill(dsLoans)

            MyConn.Close()

            Dim iLoanCounter As Integer = dsLoans.Tables(0).Rows.Count

            If iLoanCounter > 0 Then
                sqlLoan = " and h.loanid in ( "
            End If
            For i = 0 To iLoanCounter - 1
                dr1 = dsLoans.Tables(0).Rows(i)

                tLoanID = dr1("loanid")


                sqlLoan = sqlLoan & tLoanID & ", "
                SomethingSupplied = 1
            Next
            If iLoanCounter > 0 Then
                sqlLoan = sqlLoan & " 0) "  'there are no 0 loans so this will not pick up anything extra
            End If

        End If

        If IsNumeric(frmLoanID.Text) Then
            iLoanID = frmLoanID.Text
            strConn = ConfigurationManager.ConnectionStrings(connection).ConnectionString
            MyConn = New FirebirdSql.Data.FirebirdClient.FbConnection(strConn)
            MyConn.Open()

            MySQL = "select l.loanid, trim(l.business_name) as business_name
                  from loans l
                  where l.loanid = " & iLoanID


            dsLoans = New DataSet
            Adaptor = New FirebirdSql.Data.FirebirdClient.FbDataAdapter(MySQL, MyConn)

            Adaptor.Fill(dsLoans)

            MyConn.Close()

            Dim iLoanCounter As Integer = dsLoans.Tables(0).Rows.Count

            For i = 0 To iLoanCounter - 1
                dr1 = dsLoans.Tables(0).Rows(i)

                xBusinessName = dr1("business_name")

                frmLoanName.Text = xBusinessName
                sqlLoan = " and h.loanid =  " & iLoanID
                SomethingSupplied = 1
            Next
        End If

        sqlLHID = ""
        If IsNumeric(frmLHID.Text) Then
            strConn = ConfigurationManager.ConnectionStrings(connection).ConnectionString
            MyConn = New FirebirdSql.Data.FirebirdClient.FbConnection(strConn)
            iLHID = frmLHID.Text

            sqlLHID = " and h.loan_holdings_id =  " & iLHID
            SomethingSupplied = 1

        End If

        If SomethingSupplied = 1 Then
            MyConn.Open()

            'MySQL = "select distinct o.orderid, o.lh_id, h.loanid, trim(l.business_name) as business_name 
            '        from orders o, loan_holdings h, loans l
            '        where o.accountid = " & iAccountid &
            '        " and o.lh_id = h.loan_holdings_id
            '        and h.loanid = l.loanid
            '         order by h.loanid, h.loan_holdings_id, o.orderid "

            MySQL = "Select orderid, lh_id, loanid, business_name, accountid
                    from
                    (select distinct o.orderid as orderid, o.lh_id as lh_id, h.loanid as loanid, trim(l.business_name) as business_name, o.accountid as accountid
                    from orders o, loan_holdings h, loans l, accounts a, users u
                    where
                    o.lh_id = h.loan_holdings_id
                    and o.accountid = a.accountid
                    and u.userid = a.userid
                    and u.usertype = 0
                    and h.loanid = l.loanid
                    " & sqlAccount & sqlLoan & sqlLHID & "
                      union all
                    select distinct o.orderid as orderid, h.loan_holdings_id as lh_id, h.loanid as loanid, trim(l.business_name) as business_name, o.accountid as accountid
                    from orders o, loan_holdings h, loans l, lh_balances b, bids d, accounts a, users u
                    where 
                    o.lh_id = 0
                    and h.loanid = l.loanid
                    and o.loanid = h.loanid
                    and b.lh_id = h.loan_holdings_id
                    and b.accountid = o.accountid
                    and h.bidid = d.bidid
                    and o.accountid = a.accountid
                    and u.userid = a.userid
                    and u.usertype = 0
                    and o.orderid = d.orderid
                    " & sqlAccount3 & sqlLoan & sqlLHID & "
                    )   results
                    order by loanid, lh_id, accountid, orderid"

            dsOrders = New DataSet
            Adaptor = New FirebirdSql.Data.FirebirdClient.FbDataAdapter(MySQL, MyConn)

            Adaptor.Fill(dsOrders)

            MyConn.Close()
            Dim iOrderCounter As Integer = dsOrders.Tables(0).Rows.Count
            Dim prevLoanId, prevOrderid, prevLHID, thisLoanId, thisOrderid, thisLHID, thisAccountid As Integer
            Dim loanname As String
            Dim prevDate, thisDate As Date



            Dim CurrLHBal As Integer
            Dim PIBal As Integer

            For i = 0 To iOrderCounter - 1
                dr2 = dsOrders.Tables(0).Rows(i)
                thisLoanId = dr2("loanid")
                thisOrderid = dr2("orderid")
                thisLHID = dr2("lh_id")
                thisAccountID = dr2("Accountid")



                If thisLoanId <> printedLoanid Then
                    prevLoanId = thisLoanId
                    CurrLHBal = 0
                    PIBal = 0
                    loanname = dr2("business_name")
                    dispLoanID = thisLoanId
                    dispLoanName = loanname
                    dispLHID = prevLHID
                    dispAccountID = prevAccountid

                Else
                    dispLoanID = Nothing
                    dispLoanName = Nothing
                    dispLHID = Nothing
                    dispAccountID = prevAccountid
                End If

                If thisLHID <> printedLHID Then
                    prevLHID = thisLHID
                    CurrLHBal = 0
                    PIBal = 0
                    dispLHID = prevLHID
                Else
                    dispLHID = Nothing
                End If

                If thisAccountid <> printedAccountID Then
                    prevAccountid = thisAccountid
                    CurrLHBal = 0
                    PIBal = 0
                    dispAccountID = prevAccountid
                Else
                    dispAccountID = Nothing
                End If


                If thisOrderid <> printedOrderID Then
                    prevOrderid = thisOrderid
                    disporderid = thisOrderid
                    MySQL = "Select f.datecreated, f.amount,  f.transtype,  f.isactive
                            From fin_trans f
                            Where  
                            f.orderid = " & thisOrderid &
                            " and f.isactive = 0
                            and f.accountid = " & thisAccountid &
                            " and f.transtype not in (1200, 1201, 1203, 1204, 1300, 1302)  
                            order by f.datecreated, f.transtype "

                    dsTrans = New DataSet
                    Adaptor = New FirebirdSql.Data.FirebirdClient.FbDataAdapter(MySQL, MyConn)

                    Adaptor.Fill(dsTrans)

                    MyConn.Close()
                    Dim iTransCounter As Integer = dsTrans.Tables(0).Rows.Count
                    For j = 0 To iTransCounter - 1
                        Dim newExtract As New Extract

                        dr3 = dsTrans.Tables(0).Rows(j)
                        newExtract.AccountID = dispAccountID
                        newExtract.LoanID = dispLoanID
                        newExtract.LoanName = dispLoanName
                        newExtract.LHID = dispLHID
                        newExtract.OrderID = disporderid

                        thisDate = dr3("datecreated")
                        If thisDate = prevDate Then
                            dispDate = Nothing
                        Else
                            dispDate = dr3("datecreated")
                        End If
                        newExtract.TransactionDate = dispDate
                        prevDate = thisDate
                        newExtract.TransType = dr3("transtype")
                        Dim itranstype As Integer = dr3("transtype")
                        Dim xtransdesc As String = Nothing
                        Dim xIO As String = Nothing
                        Transtype(itranstype, xtransdesc, xIO)
                        newExtract.TransDesc = xtransdesc
                        If xIO = "I" Then
                            newExtract.LHAmtIn = dr3("amount")
                            newExtract.LHAmtOut = ""
                            CurrLHBal += dr3("amount")
                        End If
                        If xIO = "O" Then
                            newExtract.LHAmtIn = ""
                            newExtract.LHAmtOut = dr3("amount")
                            CurrLHBal -= dr3("amount")
                        End If
                        If itranstype = (1408 Or 1412) Then
                            PIBal += dr3("amount")
                        End If
                        If itranstype = (1409 Or 1410) Then
                            PIBal -= dr3("amount")
                        End If
                        newExtract.Amount = dr3("amount")
                        newExtract.LHBal = CurrLHBal
                        newExtract.PIBal = PIBal
                        ExtractList.Add(newExtract)
                        disporderid = Nothing
                        dispLoanID = Nothing
                        dispLHID = Nothing
                        dispLoanName = Nothing
                        dispAccountid = Nothing
                        printedLoanid = thisLoanId
                        printedOrderID = thisOrderid
                        printedLHID = thisLHID
                        printedAccountID = thisAccountid

                    Next
                End If



            Next

            DataGridView1.DataSource = ExtractList
            DataGridView1.Columns(0).Width = 50
            DataGridView1.Columns(1).Width = 50
            DataGridView1.Columns(2).Width = 240
            DataGridView1.Columns(3).Width = 50
            DataGridView1.Columns(4).Width = 50
            DataGridView1.Columns(5).Width = 120
            DataGridView1.Columns(6).Width = 60
            DataGridView1.Columns(7).Width = 240
            DataGridView1.Columns(8).Width = 80
            DataGridView1.Columns(9).Width = 80
            DataGridView1.Columns(10).Width = 80
            DataGridView1.Columns(11).Width = 80
            DataGridView1.Columns(12).Width = 80

        End If
    End Sub

    Public Function Transtype(ByVal iTranstype As Integer, ByRef xtransdesc As String, ByRef xIO As String)
        Select Case iTranstype
            Case 1206
                xtransdesc = "Auction Completion"
                xIO = "I"
            Case 1208
                xtransdesc = "Auction Drawdown"
                xIO = "I"
            Case 1301
                xtransdesc = "Capital on Loan Repayment"
                xIO = "O"
            Case 1303
                xtransdesc = "Earned Interest Received"
                xIO = "x"
            Case 1304
                xtransdesc = "Facility Fee Paid"
                xIO = "x"
            Case 1400
                xtransdesc = "Loan Part Sale"
                xIO = "O"
            Case 1401
                xtransdesc = "Loan Part Purchase"
                xIO = "I"
            Case 1402
                xtransdesc = "Transaction Fees Paid"
                xIO = "x"
            Case 1406
                xtransdesc = "Transaction Fee Paid"
                xIO = "x"
            Case 1408
                xtransdesc = "Purchased Interest Bought"
                xIO = "x"
            Case 1409
                xtransdesc = "Repayment of Purchased Interest"
                xIO = "x"
            Case 1410
                xtransdesc = "Purchased Interest on Repayment"
                xIO = "x"
            Case 1411
                xtransdesc = "Earned Interest on Repayment"
                xIO = "x"
            Case 1412
                xtransdesc = "Purchase of Earned Interest"
                xIO = "x"
            Case 1413
                xtransdesc = "Earned Interest Received"
                xIO = "x"

            Case Else
                xtransdesc = ""
                xIO = ""
        End Select

    End Function

End Class
