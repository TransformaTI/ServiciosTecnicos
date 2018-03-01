Imports System.Data.SqlClient
Public Class frmAsignar
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Pedido As Integer, ByVal Celula As Integer, ByVal AñoPed As Integer, ByVal FCompromiso As DateTime, ByVal Usuario As String)
        MyBase.New()
        _Pedido = Pedido
        _Celula = Celula
        _AñoPed = AñoPed
        _FCompromiso = FCompromiso
        _Usuario = Usuario
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
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpFAsignacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblcelula As System.Windows.Forms.Label
    Friend WithEvents lblPedidoReferencia As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboUnidad As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnAceptar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblFranquicia As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmAsignar))
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpFAsignacion = New System.Windows.Forms.DateTimePicker()
        Me.lblcelula = New System.Windows.Forms.Label()
        Me.lblPedidoReferencia = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboUnidad = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.btnAceptar = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.lblFranquicia = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 14)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "FAsignación:"
        '
        'dtpFAsignacion
        '
        Me.dtpFAsignacion.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFAsignacion.Location = New System.Drawing.Point(96, 112)
        Me.dtpFAsignacion.Name = "dtpFAsignacion"
        Me.dtpFAsignacion.Size = New System.Drawing.Size(112, 20)
        Me.dtpFAsignacion.TabIndex = 43
        Me.dtpFAsignacion.Value = New Date(2004, 4, 28, 17, 13, 1, 500)
        '
        'lblcelula
        '
        Me.lblcelula.BackColor = System.Drawing.SystemColors.Window
        Me.lblcelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblcelula.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcelula.Location = New System.Drawing.Point(304, 82)
        Me.lblcelula.Name = "lblcelula"
        Me.lblcelula.Size = New System.Drawing.Size(104, 21)
        Me.lblcelula.TabIndex = 42
        Me.lblcelula.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPedidoReferencia
        '
        Me.lblPedidoReferencia.BackColor = System.Drawing.SystemColors.Window
        Me.lblPedidoReferencia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPedidoReferencia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPedidoReferencia.Location = New System.Drawing.Point(96, 80)
        Me.lblPedidoReferencia.Name = "lblPedidoReferencia"
        Me.lblPedidoReferencia.Size = New System.Drawing.Size(112, 24)
        Me.lblPedidoReferencia.TabIndex = 41
        Me.lblPedidoReferencia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 85)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 14)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Pedido:"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(224, 84)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 16)
        Me.Label6.TabIndex = 47
        Me.Label6.Text = "Celula:"
        '
        'cboUnidad
        '
        Me.cboUnidad.Location = New System.Drawing.Point(304, 112)
        Me.cboUnidad.Name = "cboUnidad"
        Me.cboUnidad.Size = New System.Drawing.Size(112, 21)
        Me.cboUnidad.TabIndex = 45
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(224, 115)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 14)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Unidad:"
        '
        'ToolBar1
        '
        Me.ToolBar1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAceptar, Me.btnCerrar})
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(424, 41)
        Me.ToolBar1.TabIndex = 48
        '
        'btnAceptar
        '
        Me.btnAceptar.ImageIndex = 0
        Me.btnAceptar.Text = "Aceptar"
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
        'lblFranquicia
        '
        Me.lblFranquicia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFranquicia.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFranquicia.Location = New System.Drawing.Point(280, 144)
        Me.lblFranquicia.Name = "lblFranquicia"
        Me.lblFranquicia.Size = New System.Drawing.Size(136, 23)
        Me.lblFranquicia.TabIndex = 49
        Me.lblFranquicia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.ForestGreen
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label3.Location = New System.Drawing.Point(0, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(424, 24)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "Asignación de pedidos"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmAsignar
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.YellowGreen
        Me.ClientSize = New System.Drawing.Size(424, 174)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label3, Me.lblFranquicia, Me.ToolBar1, Me.Label6, Me.cboUnidad, Me.Label2, Me.Label4, Me.dtpFAsignacion, Me.lblcelula, Me.lblPedidoReferencia, Me.Label1})
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAsignar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Asignar"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Guarda As Integer
    Public _Pedido As Integer
    Public _Celula As Integer
    Public _AñoPed As Integer
    Public _AñoAtt As Integer
    Public _Folio As Integer
    Public _StatusLogistica As String
    Public _FCompromiso As Date
    Public _FAsignacion As Date
    Public _Usuario As String
    Public Llena As Integer
    Dim _Chofer As String


    Private Sub llenacombo()
        Dim da As New SqlDataAdapter("select autotanque,folio from autotanqueturno where TIPOPRODUCTO = 2 and FInicioRuta >= ' " & dtpFAsignacion.Value.ToShortDateString & " 00:00:00 ' " _
                   & " and FInicioRuta <= ' " & dtpFAsignacion.Value.ToShortDateString & " 23:59:59 ' ", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim dt As New DataTable("Unidad")
            da.Fill(dt)
            With cboUnidad
                .DataSource = dt
                .DisplayMember = "autotanque"
                .ValueMember = "autotanque"
            End With
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
        Llena = 1


    End Sub

    Private Sub llenaFolioAutotanque()
        If cnnSigamet.State = ConnectionState.Open Then
            cnnSigamet.Close()
        Else
        End If
        Dim strQuery As String = "select isnull(f.Nombre,'s/Franquicia') as Nombre,Chofer,att.autotanque,folio,AñoAtt,StatusLogistica,FInicioRuta, case when a.Franquicia is not null then 'Franquicia' else 'Servicio Tecnico' end as Franquicia from autotanqueturno att left join autotanque a on att.autotanque = a.autotanque left join franquicia f on a.franquicia = f.franquicia where att.TIPOPRODUCTO = 2 and fInicioRuta >= ' " & dtpFAsignacion.Value.ToShortDateString & " 00:00:00 ' " _
                   & " and fInicioRuta <= ' " & dtpFAsignacion.Value.ToShortDateString & " 23:59:59 ' " _
                   & " and att.autotanque = ' " & cboUnidad.Text & "  ' "
        Dim daFolioAuto As New SqlDataAdapter(strQuery, cnnSigamet)
        Dim dtFolio As New DataTable("FolioAuto")
        daFolioAuto.Fill(dtFolio)
        If dtFolio.Rows.Count > 0 Then
            _Folio = CType(dtFolio.Rows(0).Item("folio"), Integer)
            _AñoAtt = CType(dtFolio.Rows(0).Item("añoatt"), Integer)
            _StatusLogistica = RTrim(CType(dtFolio.Rows(0).Item("statuslogistica"), String))
            _FAsignacion = CType(dtFolio.Rows(0).Item("FInicioRuta"), Date).Date
            lblFranquicia.Text = CType(dtFolio.Rows(0).Item("Nombre"), String)
        End If

    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Text
            Case "Aceptar"
                If _FCompromiso.Date = _FAsignacion Then

                    If _StatusLogistica = "LIQCAJA" Then
                        MessageBox.Show("Estos datos ya fueron liquidados, seleccione la Unidad y Fecha adecuados e intente otra vez.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        Guarda = 1
                        Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                        ConexionTransaccion.Open()
                        'instancia del comando
                        Dim sqlcommandtransac As New SqlCommand()
                        'Instancia de la transaccion
                        Dim SQLTransaccion As SqlTransaction

                        'Anexamos los parametros del comando

                        sqlcommandtransac.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                        sqlcommandtransac.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AñoAtt
                        sqlcommandtransac.Parameters.Add("@AñoPed", SqlDbType.Int).Value = _AñoPed

                        sqlcommandtransac.Parameters.Add("@Celula", SqlDbType.Int).Value = _Celula
                        If cboUnidad.SelectedValue Is Nothing Then
                            sqlcommandtransac.Parameters.Add("@Unidad", SqlDbType.Int).Value = 0
                        Else
                            sqlcommandtransac.Parameters.Add("@Unidad", SqlDbType.Int).Value = cboUnidad.SelectedValue
                        End If
                        If _Folio = 0 Then
                            sqlcommandtransac.Parameters.Add("@Folio", SqlDbType.Int).Value = 0
                        Else
                            sqlcommandtransac.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
                        End If

                        sqlcommandtransac.Parameters.Add("@Tipo", SqlDbType.Int).Value = Guarda
                        sqlcommandtransac.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario



                        'Asigna el comando de inicio de transaccion 
                        SQLTransaccion = ConexionTransaccion.BeginTransaction
                        'Arma la conexion para la transaccion
                        sqlcommandtransac.Connection = ConexionTransaccion
                        'Inicio de la transaccion
                        sqlcommandtransac.Transaction = SQLTransaccion
                        Try
                            'Construccion del comando
                            sqlcommandtransac.CommandType = CommandType.StoredProcedure
                            sqlcommandtransac.CommandTimeout = 300
                            sqlcommandtransac.CommandText = "spSTModificaFCompromisoPedidoServiciosTecnicos"
                            'Ejecuta el comando en modo ExecuteNonQuery
                            sqlcommandtransac.ExecuteNonQuery()
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
                    End If
                Else
                    MessageBox.Show("La Fecha de asignacion no corresponde con la fecha compromiso del pedido, seleccione el día adecuado para asignar.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Case "Cerrar"
                If MessageBox.Show("¿Desea salir de Programación?", "Servicios Técnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Else
                    Me.Close()
                End If
        End Select
    End Sub

    Private Sub frmAsignar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SigametST.cnnSigamet.Close()
        dtpFAsignacion.Value = Now.Date
        'llenacombo()
        'llenaFolioAutotanque()
        lblcelula.Text = CType(_Celula, String)
        lblPedidoReferencia.Text = CType(_Pedido, String)
    End Sub

    Private Sub dtpFAsignacion_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFAsignacion.ValueChanged
        llenacombo()
        llenaFolioAutotanque()
    End Sub

    Private Sub cboUnidad_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboUnidad.SelectedIndexChanged
        llenaFolioAutotanque()
    End Sub
End Class
