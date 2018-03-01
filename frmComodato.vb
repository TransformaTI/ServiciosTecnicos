Imports System.Data.SqlClient
Public Class frmComodato
    Inherits System.Windows.Forms.Form

    Dim _Cliente As Integer
    Dim Lleno As Boolean
    Dim _Tipo As Byte
    Dim _Usuario As String
    Dim _Puntos As Integer
    Dim _Pedido As Integer
    Dim _Celula As Integer
    Dim _AñoPed As Integer
    Dim _PuntosCelula As Integer
    Dim TotalPuntos As Integer
    Dim FechaInicio As DateTime
    Dim _FCompromiso As DateTime
    Dim _MañanaEsDiaFestivo As Boolean
    Private Puntos As Integer
    Dim oConfig As New SigaMetClasses.cConfig(11)
    Dim _FechaCorte As Date
    Dim _FechaCorteFin As Date
    Dim _Secuencia As Integer

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Cliente As Integer, ByVal Tipo As Byte, ByVal Usuario As String, ByVal Secuencia As Integer)
        MyBase.New()
        _Cliente = Cliente
        _Tipo = Tipo
        _Usuario = Usuario
        _Secuencia = Secuencia
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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtserie As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboTipoPropiedad As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboEquipo As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents gbComodato As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpFInicioComodato As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFFinComodato As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtConsumo As System.Windows.Forms.TextBox
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents dtpFFabricacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnModificar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmComodato))
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.btnAceptar = New System.Windows.Forms.ToolBarButton()
        Me.btnModificar = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.dtpFFabricacion = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtserie = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboTipoPropiedad = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboEquipo = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.gbComodato = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.txtConsumo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpFFinComodato = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpFInicioComodato = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.gbComodato.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAceptar, Me.btnModificar, Me.btnCerrar})
        Me.ToolBar1.ButtonSize = New System.Drawing.Size(70, 36)
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(498, 40)
        Me.ToolBar1.TabIndex = 273
        '
        'btnAceptar
        '
        Me.btnAceptar.ImageIndex = 0
        Me.btnAceptar.Text = "Aceptar"
        '
        'btnModificar
        '
        Me.btnModificar.ImageIndex = 2
        Me.btnModificar.Text = "Modificar"
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 1
        Me.btnCerrar.Text = "Cerrar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'dtpFFabricacion
        '
        Me.dtpFFabricacion.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFFabricacion.Location = New System.Drawing.Point(88, 120)
        Me.dtpFFabricacion.Name = "dtpFFabricacion"
        Me.dtpFFabricacion.Size = New System.Drawing.Size(120, 20)
        Me.dtpFFabricacion.TabIndex = 293
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 123)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 14)
        Me.Label5.TabIndex = 292
        Me.Label5.Text = "FFabricación:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtserie
        '
        Me.txtserie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtserie.Location = New System.Drawing.Point(320, 120)
        Me.txtserie.Name = "txtserie"
        Me.txtserie.Size = New System.Drawing.Size(168, 20)
        Me.txtserie.TabIndex = 291
        Me.txtserie.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(232, 123)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 14)
        Me.Label3.TabIndex = 290
        Me.Label3.Text = "Serie:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboTipoPropiedad
        '
        Me.cboTipoPropiedad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoPropiedad.Location = New System.Drawing.Point(320, 85)
        Me.cboTipoPropiedad.Name = "cboTipoPropiedad"
        Me.cboTipoPropiedad.Size = New System.Drawing.Size(168, 21)
        Me.cboTipoPropiedad.TabIndex = 289
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(224, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 14)
        Me.Label2.TabIndex = 288
        Me.Label2.Text = "TipoPropiedad:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboEquipo
        '
        Me.cboEquipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEquipo.Location = New System.Drawing.Point(88, 85)
        Me.cboEquipo.Name = "cboEquipo"
        Me.cboEquipo.Size = New System.Drawing.Size(120, 21)
        Me.cboEquipo.TabIndex = 287
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 14)
        Me.Label1.TabIndex = 286
        Me.Label1.Text = "Equipo:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(8, 56)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(48, 14)
        Me.Label71.TabIndex = 284
        Me.Label71.Text = "Cliente:"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.Control
        Me.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.ForeColor = System.Drawing.SystemColors.MenuText
        Me.lblCliente.Location = New System.Drawing.Point(88, 53)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(120, 21)
        Me.lblCliente.TabIndex = 285
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gbComodato
        '
        Me.gbComodato.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label8, Me.cboStatus, Me.txtConsumo, Me.Label7, Me.dtpFFinComodato, Me.Label6, Me.dtpFInicioComodato, Me.Label4})
        Me.gbComodato.Location = New System.Drawing.Point(8, 152)
        Me.gbComodato.Name = "gbComodato"
        Me.gbComodato.Size = New System.Drawing.Size(480, 88)
        Me.gbComodato.TabIndex = 294
        Me.gbComodato.TabStop = False
        Me.gbComodato.Text = "Comodato"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(248, 56)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(61, 14)
        Me.Label8.TabIndex = 300
        Me.Label8.Text = "Consumo:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboStatus
        '
        Me.cboStatus.Items.AddRange(New Object() {"PENDIENTE", "CANCELADO"})
        Me.cboStatus.Location = New System.Drawing.Point(120, 53)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(112, 21)
        Me.cboStatus.TabIndex = 299
        Me.cboStatus.Text = "PENDIENTE"
        '
        'txtConsumo
        '
        Me.txtConsumo.Location = New System.Drawing.Point(352, 53)
        Me.txtConsumo.Name = "txtConsumo"
        Me.txtConsumo.Size = New System.Drawing.Size(112, 20)
        Me.txtConsumo.TabIndex = 298
        Me.txtConsumo.Text = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 56)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 14)
        Me.Label7.TabIndex = 297
        Me.Label7.Text = "Status:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'dtpFFinComodato
        '
        Me.dtpFFinComodato.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFFinComodato.Location = New System.Drawing.Point(352, 21)
        Me.dtpFFinComodato.Name = "dtpFFinComodato"
        Me.dtpFFinComodato.Size = New System.Drawing.Size(112, 20)
        Me.dtpFFinComodato.TabIndex = 296
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(248, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 14)
        Me.Label6.TabIndex = 295
        Me.Label6.Text = "FFinComodato:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'dtpFInicioComodato
        '
        Me.dtpFInicioComodato.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFInicioComodato.Location = New System.Drawing.Point(120, 21)
        Me.dtpFInicioComodato.Name = "dtpFInicioComodato"
        Me.dtpFInicioComodato.Size = New System.Drawing.Size(112, 20)
        Me.dtpFInicioComodato.TabIndex = 294
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(105, 14)
        Me.Label4.TabIndex = 293
        Me.Label4.Text = "FInicioComodato:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmComodato
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(498, 256)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.gbComodato, Me.dtpFFabricacion, Me.Label5, Me.Label3, Me.Label2, Me.Label1, Me.Label71, Me.txtserie, Me.cboTipoPropiedad, Me.cboEquipo, Me.lblCliente, Me.ToolBar1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmComodato"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Agrega Equipo"
        Me.gbComodato.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub LlenaDatos()
        Dim Consulta As New SqlCommand("SELECT Equipo,Serie,isnull(FFabricacion,'')as FFabricacion ,Tipopropiedad,falta,Status,ISNULL(Fstatus,'') AS Fstatus ,ISNULL(FInicioComodato,'') AS FInicioComodato,ISNULL(FTerminoComodato,'') AS FTerminoComodato,ISNULL(ConsumoPromedioCliente,0) AS ConsumoPromedioCliente FROM CLIENTEEQUIPO WHERE SECUENCIA = " & _Secuencia & "and CLIENTE = " & _Cliente, cnnSigamet)
        Try
            If cnnSigamet.State = ConnectionState.Open Then
                cnnSigamet.Close()
            End If
            cnnSigamet.Open()
            Dim dr As SqlDataReader = Consulta.ExecuteReader
            While dr.Read
                cboEquipo.SelectedValue = dr.Item("Equipo")
                dtpFFabricacion.Value = CType(dr.Item("fFabricacion"), Date)
                dtpFInicioComodato.Value = CType(dr.Item("FInicioComodato"), Date)
                dtpFFinComodato.Value = CType(dr.Item("FTerminoComodato"), Date)
                txtConsumo.Text = CType(dr.Item("ConsumoPromedioCliente"), String)
                txtserie.Text = CType(dr.Item("Serie"), String)
                cboStatus.SelectedItem = dr.Item("Status")
                cboTipoPropiedad.SelectedValue = dr.Item("TipoPropiedad")
            End While
        Catch exc As Exception
            MessageBox.Show(exc.Message)
        Finally
            cnnSigamet.Close()
        End Try
    End Sub

    Private Sub llenaCombo()
        Dim Llena As String = "Select Equipo,Descripcion from Equipo "
        Dim SqlEquipo As New SqlDataAdapter(Llena, cnnSigamet)
        Dim dsEquipo As New DataSet()
        SqlEquipo.Fill(dsEquipo, "Equipo")
        With cboEquipo
            .DataSource = dsEquipo.Tables("Equipo")
            .DisplayMember = "Descripcion"
            .ValueMember = "Equipo"
            .SelectedIndex = 0
        End With
        Llena = ""

        'llena combo de tipopropiedad
        If oSeguridad.TieneAcceso("TIPO PROPIEDAD") Then
            Llena = "Select TipoPropiedad,Descripcion From TipoPropiedad where tipopropiedad in (1,2)"

        Else
            Llena = "Select TipoPropiedad,Descripcion From TipoPropiedad where tipopropiedad = 1"
        End If
        Dim SqlTipoPropiedad As New SqlDataAdapter(Llena, cnnSigamet)
        Dim dsTipoPropiedad As New DataSet()
        SqlTipoPropiedad.Fill(dsTipoPropiedad, "TipoPropiedad")
        With cboTipoPropiedad
            .DataSource = dsTipoPropiedad.Tables("TipoPropiedad")
            .DisplayMember = "Descripcion"
            .ValueMember = "TipoPropiedad"
        End With
        Try
            cboTipoPropiedad.SelectedIndex = 0
        Catch e As Exception
            cboTipoPropiedad.SelectedIndex = 0
        End Try
        Llena = ""
        Lleno = True
    End Sub

    Private Sub PuntosCelula()
        Dim SumaPuntosCelula As New SqlCommand("select sum(puntos) as TotalPuntos from pedido p left join serviciotecnico st on p.pedido = st.pedido and p.celula = st.celula and p.añoped = st.añoped  left join tiposervicio ts on st.tiposervicio = ts.tiposervicio where producto = 4 and status = 'ACTIVO' and FCompromiso between '" & FechaInicio & " 00:00:00'" & " and '" & FechaInicio & " 23:59:59' " & " and p.celula = " & _Celula, cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim dr As SqlDataReader = SumaPuntosCelula.ExecuteReader
            Do While dr.Read
                _PuntosCelula = CType(dr("TotalPuntos"), Integer)
            Loop
            cnnSigamet.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Function SumaPuntos(ByVal Fecha As Date, _
                            ByVal Celula As Byte) As Integer
        Dim strQuery As String = _
        "SELECT TotalPuntos,0 FROM vwSTSumaPuntos " & _
        "where producto = 4 " & _
        "and fcompromiso >= ' " & Fecha.ToShortDateString & " 00:00:00' " & _
        "and fcompromiso <= ' " & Fecha.ToShortDateString & " 23:59:59' " & _
        "and celula = " & Celula.ToString

        Dim daSumaPuntos As New SqlDataAdapter(strQuery, cnnSigamet)
        Dim dtSumaPuntos As New DataTable("Suma")
        daSumaPuntos.Fill(dtSumaPuntos)
        If dtSumaPuntos.Rows.Count > 0 Then
            Puntos = CType(dtSumaPuntos.Rows(0).Item("totalpuntos"), Integer)
        Else
            Puntos = 0
        End If
        'Puntos = CType(dtSumaPuntos.Rows(0).Item("totalpuntos"), Integer)
        If dtSumaPuntos.Rows.Count > 0 Then
            Return CType(dtSumaPuntos.Rows(0).Item("TotalPuntos"), Integer)
        Else
            Return 0
        End If

    End Function

    Private Sub ValidaComodato()

        Dim consulta As New SqlDataAdapter("SELECT SECUENCIA,SERIE,EQUIPO, TIPOPROPIEDAD  FROM vwSTValidaClienteEquipo WHERE  CLIENTE = " & lblCliente.Text, cnnSigamet)
        Dim dt As New DataTable("ValidaComodato")
        consulta.Fill(dt)
        If dt.Rows.Count > 0 Then
            If MessageBox.Show("El cliente " & lblCliente.Text & ", ya tiene un " & CType(dt.Rows(0).Item("Equipo"), String) & " , en tipo propiedad " & CType(dt.Rows(0).Item("TipoPropiedad"), String) & ",. ¿Desea agreagr otro equipo en comodato a este cliente?", "Servicios Técnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Else
                Me.Close()
            End If
        Else
        End If
    End Sub



    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click

    End Sub

    Private Sub frmComodato_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oSeguridad = New SigaMetClasses.cSeguridad(_Usuario, 11)
        If _Tipo = 1 Then
            dtpFInicioComodato.MinDate = Now.Date
            btnModificar.Enabled = False
            lblCliente.Text = CType(_Cliente, String)
            llenaCombo()
            gbComodato.Enabled = False
        Else
            btnAceptar.Enabled = False
            lblCliente.Text = CType(_Cliente, String)
            llenaCombo()
            gbComodato.Enabled = True
            LlenaDatos()

            If oSeguridad.TieneAcceso("CANCELA EQUIPO") Then
                cboStatus.Text = "CANCELADO"
            Else
            End If
        End If

    End Sub

    Private Sub cboTipoPropiedad_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoPropiedad.SelectedIndexChanged

        If Lleno = True Then
            If _Tipo = 0 Then
            Else
                ValidaComodato()
            End If

            Dim TipoPropiedad As Integer
            TipoPropiedad = CType(cboTipoPropiedad.SelectedValue, Integer)
            If TipoPropiedad = 2 Then
                gbComodato.Enabled = True
                If oSeguridad.TieneAcceso("PANTALLA COMODATOS") Then
                    cboStatus.Enabled = True
                Else
                    cboStatus.Enabled = False
                End If
            Else
                gbComodato.Enabled = False
            End If

        Else
        End If

    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Text
            Case "Aceptar"

                Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                ConexionTransaccion.Open()
                'instancia del comando
                Dim SQLCommandTransac As New SqlCommand()
                'Instancia de la transaccion
                Dim SQLTransaccion As SqlTransaction

                'Anexamos los parametros del comando
                SQLCommandTransac.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
                SQLCommandTransac.Parameters.Add("@Serie", SqlDbType.Char).Value = txtserie.Text
                SQLCommandTransac.Parameters.Add("@Equipo", SqlDbType.Int).Value = CType(cboEquipo.SelectedValue, Integer)
                SQLCommandTransac.Parameters.Add("@TipoPropiedad", SqlDbType.Int).Value = CType(cboTipoPropiedad.SelectedValue, Integer)
                SQLCommandTransac.Parameters.Add("@FFabricacion", SqlDbType.DateTime).Value = dtpFFabricacion.Value.Date
                SQLCommandTransac.Parameters.Add("@FInicioComodato", SqlDbType.DateTime).Value = dtpFInicioComodato.Value
                SQLCommandTransac.Parameters.Add("@FFinComodato", SqlDbType.DateTime).Value = dtpFFinComodato.Value
                SQLCommandTransac.Parameters.Add("@StatusComodato", SqlDbType.Char).Value = cboStatus.SelectedItem
                SQLCommandTransac.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                SQLCommandTransac.Parameters.Add("@Tipo", SqlDbType.Int).Value = _Tipo
                If txtConsumo.Text = "" Then
                    SQLCommandTransac.Parameters.Add("@Consumopromedio", SqlDbType.Int).Value = 0
                Else
                    SQLCommandTransac.Parameters.Add("@Consumopromedio", SqlDbType.Int).Value = txtConsumo.Text
                End If


                SQLCommandTransac.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = Now.Date
                SQLCommandTransac.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                SQLCommandTransac.Parameters.Add("@Celula", SqlDbType.Int).Value = _Celula
                SQLCommandTransac.Parameters.Add("@Añoped", SqlDbType.Int).Value = _AñoPed


                'Asigna el comando de inicio de transaccion
                SQLTransaccion = ConexionTransaccion.BeginTransaction
                'Arma la conexion para la transaccion
                SQLCommandTransac.Connection = ConexionTransaccion
                'Inicio de la transaccion
                SQLCommandTransac.Transaction = SQLTransaccion
                Try
                    'Construccion del comando
                    SQLCommandTransac.CommandType = CommandType.StoredProcedure

                    SQLCommandTransac.CommandText = "spSTClienteEquipoAlta"


                    'Ejecuta el comando en modo ExecuteNonQuery
                    SQLCommandTransac.ExecuteNonQuery()
                    'Transaccion Exitosa
                    SQLTransaccion.Commit()
                Catch eException As Exception
                    'En caso de error rollback a la operacion
                    SQLTransaccion.Rollback()
                    MsgBox(eException.Message)
                Finally
                    'Fin de la transaccion
                    ConexionTransaccion.Close()
                    'ConexionTransaccion.Dispose()
                    Me.Close()
                End Try
            Case "Modificar"

                Dim Status As String

                Dim consulta As New SqlDataAdapter("select Status from ClienteEquipo where Secuencia = " & _Secuencia & "and Cliente = " & lblCliente.Text, cnnSigamet)
                Dim dtStatus As New DataTable("Status")
                consulta.Fill(dtStatus)
                If dtStatus.Rows.Count > 0 Then
                    Status = CType(dtStatus.Rows(0).Item("Status"), String)
                    If Status = "CANCELADO" Then
                        MessageBox.Show("Usted no puede cancelar el Equipo, pues ya esta en status CANCELADO", "Equipo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                    Else
                        If MessageBox.Show("¿Esta usted seguro de cancelar el equipo " & cboEquipo.Text & ", con tipo propiedad " & cboTipoPropiedad.Text.Trim & ".?", "Equipo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Else
                        End If
                    End If
                Else
                End If



                'Dim ConexionTransaccion As New SqlConnection(ConString)
                'conexiontransaccion.Open()
                'Dim SQlComando As New SqlCommand()
                'Dim SQLTransaccion As SqlTransaction

                'SQlComando.Parameters.Add("@Serie", SqlDbType.Char).Value = txtserie.Text
                'SQlComando.Parameters.Add("@Equipo", SqlDbType.Int).Value = CType(cboEquipo.SelectedValue, Integer)
                'SQlComando.Parameters.Add("@Tipo", SqlDbType.Int).Value = _Tipo
                'SQlComando.Parameters.Add("@StatusComodato", SqlDbType.Char).Value = cboStatus.SelectedItem
                'SQlComando.Parameters.Add("@TipoPropiedad", SqlDbType.Int).Value = CType(cboTipoPropiedad.SelectedValue, Integer)
                'SQlComando.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                'SQlComando.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
                'SQlComando.Parameters.Add("@FFabricacion", SqlDbType.DateTime).Value = dtpFFabricacion.Value.Date
                'SQlComando.Parameters.Add("@FInicioComodato", SqlDbType.DateTime).Value = dtpFInicioComodato.Value

                'SQlComando.Parameters.Add("@FFinComodato", SqlDbType.DateTime).Value = dtpFFinComodato.Value

                'sqltransaccion = conexiontransaccion.BeginTransaction
                'SQlComando.Connection = conexiontransaccion
                'SQlComando.Transaction = sqltransaccion
                'Try
                '    SQlComando.CommandType = CommandType.StoredProcedure
                '    SQlComando.CommandText = "spSTClienteEquipoAlta"
                '    SQlComando.ExecuteNonQuery()
                '    sqltransaccion.Commit()
                'Catch exc As Exception
                '    sqltransaccion.Rollback()
                '    MessageBox.Show("Hay un error en los datos" + exc.Message, "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'Finally
                '    conexiontransaccion.Close()
                '    conexiontransaccion.Dispose()
                '    Me.Close()
                'End Try


            Case "Cancelar"
                Me.Close()
        End Select
    End Sub
End Class
