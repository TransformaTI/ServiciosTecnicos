Option Strict On
Imports System.Data.SqlClient
Public Class frmChequeST
    Inherits System.Windows.Forms.Form
    Dim _PedidoReferencia As String
    Dim _Pedido As Integer
    Dim _Celula As Integer
    Dim _AñoPed As Integer
    Dim _Total As Double
    Dim _Saldo As Double

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PedidoReferencia As String)
        MyBase.New()
        _PedidoReferencia = PedidoReferencia
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents btnAceptar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnModificar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCancelarCheque As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnAcepta As System.Windows.Forms.Button
    Friend WithEvents btnleer As System.Windows.Forms.Button
    Friend WithEvents txtcodigo As System.Windows.Forms.TextBox
    Friend WithEvents txtSaldo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCliente As System.Windows.Forms.TextBox
    Friend WithEvents txtMonto As System.Windows.Forms.TextBox
    Friend WithEvents dtpFCheque As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtCuenta As System.Windows.Forms.TextBox
    Friend WithEvents txtCheque As System.Windows.Forms.TextBox
    Friend WithEvents cboBanco As System.Windows.Forms.ComboBox
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmChequeST))
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.btnAceptar = New System.Windows.Forms.ToolBarButton()
        Me.btnModificar = New System.Windows.Forms.ToolBarButton()
        Me.btnCancelarCheque = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnAcepta = New System.Windows.Forms.Button()
        Me.btnleer = New System.Windows.Forms.Button()
        Me.txtcodigo = New System.Windows.Forms.TextBox()
        Me.txtSaldo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCliente = New System.Windows.Forms.TextBox()
        Me.txtMonto = New System.Windows.Forms.TextBox()
        Me.dtpFCheque = New System.Windows.Forms.DateTimePicker()
        Me.txtCuenta = New System.Windows.Forms.TextBox()
        Me.txtCheque = New System.Windows.Forms.TextBox()
        Me.cboBanco = New System.Windows.Forms.ComboBox()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAceptar, Me.btnModificar, Me.btnCancelarCheque, Me.btnCerrar})
        Me.ToolBar1.ButtonSize = New System.Drawing.Size(67, 36)
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(456, 40)
        Me.ToolBar1.TabIndex = 27
        '
        'btnAceptar
        '
        Me.btnAceptar.ImageIndex = 0
        Me.btnAceptar.Text = "Aceptar"
        '
        'btnModificar
        '
        Me.btnModificar.ImageIndex = 1
        Me.btnModificar.Text = "Modificar"
        '
        'btnCancelarCheque
        '
        Me.btnCancelarCheque.ImageIndex = 2
        Me.btnCancelarCheque.Text = "Cancelar"
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 3
        Me.btnCerrar.Text = "Salir"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'btnAcepta
        '
        Me.btnAcepta.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAcepta.Location = New System.Drawing.Point(392, 216)
        Me.btnAcepta.Name = "btnAcepta"
        Me.btnAcepta.Size = New System.Drawing.Size(24, 16)
        Me.btnAcepta.TabIndex = 47
        '
        'btnleer
        '
        Me.btnleer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnleer.Location = New System.Drawing.Point(424, 216)
        Me.btnleer.Name = "btnleer"
        Me.btnleer.Size = New System.Drawing.Size(24, 16)
        Me.btnleer.TabIndex = 46
        '
        'txtcodigo
        '
        Me.txtcodigo.BackColor = System.Drawing.SystemColors.Control
        Me.txtcodigo.Location = New System.Drawing.Point(224, 232)
        Me.txtcodigo.Name = "txtcodigo"
        Me.txtcodigo.Size = New System.Drawing.Size(224, 20)
        Me.txtcodigo.TabIndex = 31
        Me.txtcodigo.Text = ""
        '
        'txtSaldo
        '
        Me.txtSaldo.BackColor = System.Drawing.SystemColors.Control
        Me.txtSaldo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaldo.Location = New System.Drawing.Point(104, 232)
        Me.txtSaldo.MaxLength = 20
        Me.txtSaldo.Name = "txtSaldo"
        Me.txtSaldo.ReadOnly = True
        Me.txtSaldo.Size = New System.Drawing.Size(104, 21)
        Me.txtSaldo.TabIndex = 36
        Me.txtSaldo.Text = "0"
        Me.txtSaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 236)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 45
        Me.Label7.Text = "Saldo :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 204)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Monto :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 172)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 13)
        Me.Label4.TabIndex = 42
        Me.Label4.Text = "Numero cuenta :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 140)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Fecha cheque :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 13)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Numero cheque :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "Banco :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 13)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = "Cliente :"
        '
        'txtCliente
        '
        Me.txtCliente.BackColor = System.Drawing.SystemColors.Control
        Me.txtCliente.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCliente.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtCliente.Location = New System.Drawing.Point(104, 48)
        Me.txtCliente.MaxLength = 20
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.Size = New System.Drawing.Size(104, 22)
        Me.txtCliente.TabIndex = 38
        Me.txtCliente.Text = ""
        Me.txtCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMonto
        '
        Me.txtMonto.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtMonto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonto.Location = New System.Drawing.Point(104, 200)
        Me.txtMonto.MaxLength = 20
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.Size = New System.Drawing.Size(104, 21)
        Me.txtMonto.TabIndex = 35
        Me.txtMonto.Text = ""
        Me.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dtpFCheque
        '
        Me.dtpFCheque.Location = New System.Drawing.Point(104, 136)
        Me.dtpFCheque.Name = "dtpFCheque"
        Me.dtpFCheque.Size = New System.Drawing.Size(213, 20)
        Me.dtpFCheque.TabIndex = 33
        '
        'txtCuenta
        '
        Me.txtCuenta.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCuenta.Location = New System.Drawing.Point(104, 168)
        Me.txtCuenta.MaxLength = 20
        Me.txtCuenta.Name = "txtCuenta"
        Me.txtCuenta.Size = New System.Drawing.Size(213, 20)
        Me.txtCuenta.TabIndex = 34
        Me.txtCuenta.Text = ""
        '
        'txtCheque
        '
        Me.txtCheque.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtCheque.Location = New System.Drawing.Point(104, 104)
        Me.txtCheque.MaxLength = 20
        Me.txtCheque.Name = "txtCheque"
        Me.txtCheque.Size = New System.Drawing.Size(213, 20)
        Me.txtCheque.TabIndex = 32
        Me.txtCheque.Text = ""
        '
        'cboBanco
        '
        Me.cboBanco.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.cboBanco.DisplayMember = "Nombre"
        Me.cboBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBanco.Location = New System.Drawing.Point(104, 72)
        Me.cboBanco.Name = "cboBanco"
        Me.cboBanco.Size = New System.Drawing.Size(216, 21)
        Me.cboBanco.TabIndex = 37
        Me.cboBanco.ValueMember = "Banco"
        '
        'lblCliente
        '
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblCliente.Location = New System.Drawing.Point(224, 50)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(224, 16)
        Me.lblCliente.TabIndex = 48
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmChequeST
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(456, 262)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblCliente, Me.btnAcepta, Me.btnleer, Me.txtcodigo, Me.txtSaldo, Me.Label7, Me.Label5, Me.Label4, Me.Label3, Me.Label2, Me.Label1, Me.Label6, Me.txtCliente, Me.txtMonto, Me.dtpFCheque, Me.txtCuenta, Me.txtCheque, Me.cboBanco, Me.ToolBar1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmChequeST"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cheque"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub DatosCliente()
        Dim SqlComando As New SqlCommand("Select Pedido,Celula,Añoped,Total,Cliente,Nombre from vwSTImprimeOrdendeServicio where PedidoReferencia = '" & _PedidoReferencia & "'", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim dr As SqlDataReader = SqlComando.ExecuteReader
            Do While dr.Read
                txtCliente.Text = CType(dr("Cliente"), String)
                lblCliente.Text = CType(dr("Nombre"), String)
                _Pedido = CType(dr("Pedido"), Integer)
                _Celula = CType(dr("Celula"), Integer)
                _AñoPed = CType(dr("AñoPed"), Integer)
                _Total = CType(dr("Total"), Integer)
            Loop

            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub

    Private Sub LlenaCombo()
        Dim da As New SqlDataAdapter("Select Banco,Nombre from Banco Where Status = 'Activo'", cnnSigamet)
        Dim dt As New DataTable("FormaPago")
        da.Fill(dt)
        With cboBanco
            .DataSource = dt
            .DisplayMember = "Nombre"
            .ValueMember = "Banco"
        End With
    End Sub

    Private Sub btnLeer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnleer.Click
        Dim strCodigo As String = txtcodigo.Text.Trim
        Dim NumeroCuenta As String
        Dim NumeroCheque As String
        NumeroCuenta = Mid(strCodigo, 16, 11)
        NumeroCheque = Mid(strCodigo, 28, 7)

        txtCuenta.Text = NumeroCuenta
        txtCheque.Text = NumeroCheque
        txtcodigo.Text = ""
    End Sub
    Private Sub txtCodigo_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcodigo.Enter
        Me.AcceptButton = btnleer
    End Sub

    Private Sub txtCodigo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcodigo.Leave
        Me.AcceptButton = btnAcepta
    End Sub

    Private Sub frmChequeST_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DatosCliente()
        LlenaCombo()
    End Sub

    Private Sub txtCheque_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCheque.Validated
        If Len(txtCheque.Text) < 7 Then
            MessageBox.Show("Debe de capturar 7 digitos", "Servicios Técnicos", MessageBoxButtons.OK)
        Else
        End If
    End Sub

    'Public Sub LLenaTabla()
    '    'Dim drCheque As DataRow = dtCheque.NewRow
    '    Main.drCheque = Main.dtCheque.NewRow
    '    'Configura todas las columnas
    '    Main.drCheque("Cliente") = txtCliente.Text
    '    Main.drCheque("NumCheque") = txtCheque.Text
    '    Main.drCheque("Numcuenta") = txtCuenta.Text
    '    Main.drCheque("fcheque") = dtpFCheque.Text
    '    Main.drCheque("Monto") = txtMonto.Text
    '    Main.drCheque("Saldo") = txtSaldo.Text
    '    Main.drCheque("Pedido") = _Pedido
    '    Main.drCheque("Celula") = _Celula
    '    Main.drCheque("AñoPed") = _AñoPed
    '    Main.dtCheque.Rows.Add(Main.drCheque)
    '    'txtCliente.Text = ""
    '    'txtCheque.Text = ""
    '    'txtCuenta.Text = ""
    '    'txtMonto.Text = ""
    '    'txtSaldo.Text = ""
    'End Sub


    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Text
            Case "Aceptar"
                If txtCliente.Text = "" Then
                    MsgBox("Teclee un numero de cliente.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    Exit Sub
                End If

                If txtCheque.Text = "" Then
                    MsgBox("Teclee un numero de cheque.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    Exit Sub
                End If

                If txtCuenta.Text = "" Then
                    MsgBox("Teclee un numero de cuenta.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    Exit Sub
                End If

                If txtMonto.Text = "" Then
                    MsgBox("Teclee un monto para el cheque.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    Exit Sub
                End If
                'LLenaTabla()
                Me.Close()
            Case "Modificar"
            Case "Cancelar"
            Case "Salir"
        End Select
    End Sub



    Private Sub txtMonto_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMonto.Validated
        _Saldo = _Total - CType(txtMonto.Text, Double)
        txtSaldo.Text = CType(_Saldo, String)
    End Sub


    Private Sub txtCheque_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCheque.TextChanged

    End Sub

    Private Sub txtMonto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMonto.TextChanged

    End Sub

    Private Sub txtCliente_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCliente.TextChanged

    End Sub
End Class
