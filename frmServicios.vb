Option Strict On
Imports System.Data.SqlClient

Public Class frmServicios
    Inherits System.Windows.Forms.Form

    Private CargaCombos As Boolean
    Private _Usuario As String
    Private DatosCargados As Boolean = False
    Private Puntos As Integer
    Public ImporteLetra As String
    Private _FPedido As Date
    Private _AñoFolioPresupuesto As Integer
    Private _Pedido As Integer
    Private _celula As Integer
    Private _AñoPed As Integer
    Private _PedidoReferencia As String
    Private _StatusServicioTecnico As String
    Private _Presupuesto As String
    Public TipoPedido As Integer
    Private Confirma As Boolean
    Public Suma As Integer
    Public Cuenta As Integer
    Public _Material As Integer
    Public _SubTotal As Integer
    Public _PuntosPermitidos As Integer
    Public TipoServicio As Integer
    Public _PuntosTipoServicio As Integer

    Public Sub New(ByVal Cliente As Integer, _
                   ByVal Fecha As Date, _
                   ByVal Usuario As String, _
                   ByVal Corporativo As Short, _
                   ByVal Sucursal As Short)


        MyBase.New()
        InitializeComponent()

        'Seguridad del modulo
        _Usuario = Usuario
        GLOBAL_Corporativo = Corporativo
        GLOBAL_Sucursal = Sucursal

        oSeguridad = New SigaMetClasses.cSeguridad(_Usuario, 11)

        dtpFCompromiso.Value = Fecha
        CargaDatos(Cliente)
        'LlenaClasificacionCliente(Cliente)

        Dim _MañanaEsDiaFestivo As Boolean
        Dim _FechaCorte As Date
        Dim _FechaCorteFin As Date
        'Dim oConfig As New SigaMetClasses.cConfig(11)
        Dim oConfig As New SigaMetClasses.cConfig(11, GLOBAL_Corporativo, GLOBAL_Sucursal)
        Dim _FechaDestino As Date
        Dim _FechaExtraordinaria As Date = _
            SigaMetClasses.CalculaFechaCardinal(Now.Year, _
            CType(Now.Month, SigaMetClasses.Enumeradores.enumMesAño), _
            SigaMetClasses.Enumeradores.enumDiaSemana.Domingo, _
            SigaMetClasses.Enumeradores.enumCardinalidad.Segundo)

        Try
            ST_HoraCorte = CType(oConfig.Parametros("HoraCorteEntreSemana"), String)
            ST_HoraCorteFin = CType(oConfig.Parametros("horacortefinsemana"), String)
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Finally
            oConfig.Dispose()

        End Try

        _FechaCorte = CType((Now.Date.ToShortDateString & " " & ST_HoraCorte), Date)
        _FechaCorteFin = CType(Now.Date.ToShortDateString & " " & ST_HoraCorteFin, Date)

        Dim strQuery As String = _
        "SELECT Año, Mes, Dia FROM Festivo WHERE Tipo = 'FESTIVO'"
        Dim da As New SqlDataAdapter(strQuery, cnnSigamet)
        Dim dt As New DataTable("Festivo")
        Try
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim dr As DataRow
                For Each dr In dt.Rows
                    If CType(dr("Dia"), Integer) = Now.Date.AddDays(1).Day And CType(dr("Mes"), Integer) = Now.Date.AddDays(1).Month Then
                        _MañanaEsDiaFestivo = True
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Finally
            da.Dispose()
        End Try


        If _FechaCorte > Now Then
            'Antes del corte
            If Not _MañanaEsDiaFestivo Then
                Select Case Now.DayOfWeek
                    Case DayOfWeek.Saturday
                        If Now.Date.AddDays(1) = _FechaExtraordinaria Then
                            _FechaDestino = Now.Date.AddDays(1)
                        Else
                            _FechaDestino = Now.Date.AddDays(2)
                        End If

                    Case Is = DayOfWeek.Sunday
                        _FechaDestino = Now.Date.AddDays(2)
                    Case Else
                        _FechaDestino = Now.Date.AddDays(1)
                End Select
            Else
                Select Case Now.DayOfWeek
                    Case DayOfWeek.Friday
                        If Now.Date.AddDays(2) = _FechaExtraordinaria Then
                            _FechaDestino = Now.Date.AddDays(2)
                        Else
                            _FechaDestino = Now.Date.AddDays(3)
                        End If
                    Case Else
                        _FechaDestino = Now.Date.AddDays(2)

                End Select

            End If
        Else
            'Despues del corte
            If Not _MañanaEsDiaFestivo Then
                Select Case Now.DayOfWeek
                    Case Is = DayOfWeek.Friday
                        If Now.Date.AddDays(2) = _FechaExtraordinaria Then
                            _FechaDestino = Now.Date.AddDays(2)
                        Else
                            _FechaDestino = Now.Date.AddDays(3)
                        End If

                    Case Is = DayOfWeek.Saturday
                        If Now.Date.AddDays(1) = _FechaExtraordinaria Then
                            _FechaDestino = Now.Date.AddDays(2)
                        Else
                            _FechaDestino = Now.Date.AddDays(3)
                        End If

                    Case Else
                        _FechaDestino = Now.Date.AddDays(2)
                End Select
            Else
                Select Case Now.DayOfWeek
                    Case DayOfWeek.Thursday
                        If Now.Date.AddDays(3) = _FechaExtraordinaria Then
                            _FechaDestino = Now.Date.AddDays(3)
                        Else
                            _FechaDestino = Now.Date.AddDays(4)
                        End If

                    Case DayOfWeek.Friday
                        If Now.Date.AddDays(2) = _FechaExtraordinaria Then
                            _FechaDestino = Now.Date.AddDays(3)
                        Else
                            _FechaDestino = Now.Date.AddDays(4)
                        End If
                    Case Else
                        _FechaDestino = Now.Date.AddDays(3)
                End Select

            End If

        End If

        'Cursor = Cursors.WaitCursor
        'Dim oServicio As New frmServicios(CType(lbContrato.Text, Integer), _FechaDestino)
        'oServicio.ShowDialog()
        'Cursor = Cursors.Default

        If oSeguridad.TieneAcceso("MODIFICA FECHA") Then
            dtpFCompromiso.Value = _FechaDestino
            'LUSATE SE MODIFICÓ PARA GOAX
            'dtpFCompromiso.MinDate = Fecha.AddMonths(-1)
        Else
            dtpFCompromiso.Value = _FechaDestino
            'LUSATE SE MODIFICÓ PARA GOAX
            'dtpFCompromiso.MinDate = _FechaDestino
        End If
        'dtpFCompromiso.Value = _FechaDestino

    End Sub

    Private Sub CargaDatos(ByVal intCliente As Integer)
        Dim da As New SqlDataAdapter("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                     "select Cliente,Celula,Ruta,RamoCliente,GiroCliente,Equipo,Nombre  from vwSTClienteServicioTecnico Where Cliente = " & intCliente.ToString & _
                                     " SET TRANSACTION ISOLATION LEVEL READ COMMITTED ", cnnSigamet)
        Dim dt As New DataTable("Cliente")
        da.Fill(dt)

        If dt.Rows.Count >= 0 Then
            lblContrato.Text = CType(dt.Rows(0).Item("Cliente"), String)
            lblcelula.Text = CType(dt.Rows(0).Item("Celula"), String)
            lblRuta.Text = CType(dt.Rows(0).Item("ruta"), String)
            lblNombreCliente.Text = CType(dt.Rows(0).Item("Nombre"), String)
        End If

    End Sub



    Private Sub LlenaCombos()

        Dim Llenacombo As String = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " _
                                   & "select tiposervicio,cast(tiposervicio as varchar (5))+' '+ts.descripcion+' '+gc.descripcion as descripcion from tiposervicio ts join girocliente gc on ts.girocliente = gc.girocliente" _
                                   & " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
        Dim SqlTipoServicio As New SqlDataAdapter(Llenacombo, cnnSigamet)
        Dim dsTipoServicio As New DataSet()
        SqlTipoServicio.Fill(dsTipoServicio, "tiposervicio")
        With cboTipoServicio
            .DataSource = dsTipoServicio.Tables("tiposervicio")
            .DisplayMember = "descripcion"
            .ValueMember = "tiposervicio"
            .SelectedIndex = 0
        End With
        Llenacombo = ""

        'llena combo material
        Llenacombo = "select material,Descripcion + '            $ ' +cast(isnull(Precio,0) as varchar (10))as descripcion FROM MATERIAL where tipo = 1"
        Dim sqlMaterial As New SqlDataAdapter(Llenacombo, cnnSigamet)
        Dim dsMaterial As New DataSet()
        sqlMaterial.Fill(dsMaterial, "materiales")
        With cboMaterial
            .DataSource = dsMaterial.Tables("materiales")
            .DisplayMember = "descripcion"
            .ValueMember = "material"
            .SelectedIndex = 0
        End With

        Llenacombo = ""



        'llena combo tipo de credito
        If oSeguridad.TieneAcceso("MODIFICA FECHA") Then
            Llenacombo = "select creditoserviciotecnico,descripcion from creditoserviciotecnico"
        Else
            Llenacombo = "select creditoserviciotecnico,descripcion from creditoserviciotecnico WHERE Administra = 1"
        End If

        Dim SQLTipoCredito As New SqlDataAdapter(Llenacombo, cnnSigamet)
        Dim dsTipoCredito As New DataSet()
        SQLTipoCredito.Fill(dsTipoCredito, "TipoCredito")
        With cboTipoCredito
            .DataSource = dsTipoCredito.Tables("TipoCredito")
            .DisplayMember = "descripcion"
            .ValueMember = "creditoserviciotecnico"
            '.SelectedIndex = 0
        End With
        Try
            cboTipoCredito.SelectedIndex = 0
        Catch e As Exception
            cboTipoCredito.SelectedIndex = 0
        End Try

        Llenacombo = ""

        'Llena combo Para TipoCargo
        If oSeguridad.TieneAcceso("TIPO PROPIEDAD") Then
            Llenacombo = "select tipopedido,descripcion from tipopedido where tipopedido in (7,8,9,10) "
        Else
            Llenacombo = "select tipopedido,descripcion from tipopedido where tipopedido in (8) "
        End If

        Dim sqlTipoPedido As New SqlDataAdapter(Llenacombo, cnnSigamet)
        Dim dsTipoPedido As New DataSet()
        sqlTipoPedido.Fill(dsTipoPedido, "tipopedido")
        With cboTipoPedido
            .DataSource = dsTipoPedido.Tables("tipopedido")
            .DisplayMember = "descripcion"
            .ValueMember = "tipopedido"
        End With
        Try
            cboTipoPedido.SelectedIndex = 1
        Catch ex As Exception
            cboTipoPedido.SelectedIndex = 0
        End Try
        Llenacombo = ""

        CargaCombos = True

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


    Private Sub CargaDatosHistoricoS()
        Dim SqlComando As New SqlCommand("select pedidoreferencia,FCompromiso,Usuario,Status,TipoServicio,Puntos  from vwSTllenaprogranmacion where tipocargo = 2 and cliente =" & lblContrato.Text, cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drHist As SqlDataReader = SqlComando.ExecuteReader
            Me.lvwHistorico.Items.Clear()
            Do While drHist.Read
                Dim oHist As ListViewItem = New ListViewItem(CType(drHist("pedidoreferencia"), String), 6)
                If Not IsDBNull(drHist("status")) Then
                    If CType(drHist("status"), String).Trim = "ATENDIDO" Then oHist.ImageIndex = 7
                    If CType(drHist("status"), String).Trim = "CANCELADO" Then oHist.ImageIndex = 8
                Else
                    oHist.ImageIndex = 6
                End If
                oHist.SubItems.Add(CType(drHist("FCompromiso"), String))
                oHist.SubItems.Add(CType(drHist("usuario"), String))
                oHist.SubItems.Add(CType(drHist("status"), String))
                oHist.SubItems.Add(CType(drHist("TipoServicio"), String))
                lvwHistorico.Items.Add(oHist)
                oHist.EnsureVisible()
            Loop
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub

    Private Sub LlenaPresupuesto()

        Dim daPresupuesto As New SqlCommand("select pedido,cliente,fPedido,FolioPresupuesto,añofoliopresupuesto,StatusPresupuesto,subtotal,descuento,total,Descuento,Total from vwSTPedidoPresupuestoServicioTecnico where pedido = " & _Pedido & " And Celula = " & _celula & " And Añoped = " & _AñoPed, cnnSigamet)
        Try
            If cnnSigamet.State = ConnectionState.Open Then
                cnnSigamet.Close()
            End If
            cnnSigamet.Open()
            Dim dr As SqlDataReader = daPresupuesto.ExecuteReader
            While dr.Read
                lblSubtotal.Text = CType(dr("subtotal"), String)
                lblDescuento.Text = CType(dr("descuento"), String)
                lblTotal.Text = CType(dr("Total"), String)
                lblStatusPresupuesto.Text = CType(dr("statuspresupuesto"), String)
                lblNumPresupuesto.Text = CType(dr("foliopresupuesto"), String)
                _FPedido = CType(dr("fpedido"), Date)
                _AñoFolioPresupuesto = CType(dr("añofoliopresupuesto"), Integer)
                lblPedido.Text = CType(dr("Pedido"), String)
            End While
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

    End Sub


    Private Sub LlenaTecnicos()
        Dim da As New SqlDataAdapter("SELECT Pedido,Autotanque,chofer" & _
                    " from vwSTPestanaServicioTecnico " & _
                    " Where celula = " & _celula & _
                    " and añoped = " & _AñoPed & " And Pedido = " & _Pedido, cnnSigamet)
        Dim dt As New DataTable("ClienteServicio")
        da.Fill(dt)
        txtTecnico.Text = CType(dt.Rows(0).Item("chofer"), String)
        lblCamioneta.Text = CType(dt.Rows(0).Item("autotanque"), String)

    End Sub

    Private Sub LlenaObservaciones()
        Dim Status As String
        _PedidoReferencia = CType(RTrim(CType(_PedidoReferencia, String)), String)
        Dim daObs As New SqlDataAdapter("select pedido,añoped,celula,tiposervicio,observaciones,ObservacionesServicioRealizado,FCompromiso,fcompromisoinciial,statusserviciotecnico from vwSTLlenaObservaciones where pedidoreferencia = '" & _PedidoReferencia & "'", cnnSigamet)
        Try

            Dim dtObs As New DataTable("observaciones")
            daObs.Fill(dtObs)
            Status = RTrim(CType(dtObs.Rows(0).Item("statusserviciotecnico"), String))
            txtObservaciones.Text = CType(dtObs.Rows(0).Item("Observaciones"), String)

            dtpFCompromiso.Value = CType(dtObs.Rows(0).Item("FCompromiso"), Date)
            lblcelula.Text = CType(dtObs.Rows(0).Item("celula"), String)


            txtTrabajoRealizado.Text = CType(dtObs.Rows(0).Item("observacionesserviciorealizado"), String)
            cboTipoServicio.SelectedValue = dtObs.Rows(0).Item("tiposervicio")
            lblHoraatencion.Text = CType(dtObs.Rows(0).Item("fcompromisoinciial"), String)

            'If Status = "ACTIVO" Then
            '    dtpFCompromiso.Value = CType(dtObs.Rows(0).Item("FCompromiso"), Date)
            'Else
            'End If

            _Pedido = CType(dtObs.Rows(0).Item("pedido"), Integer)
            _celula = CType(dtObs.Rows(0).Item("celula"), Integer)
            _AñoPed = CType(dtObs.Rows(0).Item("añoped"), Integer)

        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub

    Private Sub StatusPresupuesto()

        Dim consulta As String
        consulta = "select statuspresupuesto from vwSTStatusPresupuesto where cliente = " & lblContrato.Text
        Dim daStatus As New SqlDataAdapter(consulta, cnnSigamet)
        Dim dtStatus As New DataTable("Status")
        daStatus.Fill(dtStatus)
        If dtStatus.Rows.Count > 0 Then
            MessageBox.Show("Usted No Puede Levantar un Nuevo Presupuesto, Ya que tiene Presupuestos Pendientes", "Mensaje de Servicios Técnicos")
            btnAceptar.Enabled = True
        Else

        End If

    End Sub

    Private Sub TablaParametro()
        Dim Valor As New SqlCommand("SELECT Valor FROM PARAMETRO where modulo = 1 and parametro = 'PuntosServiciosTecnicos'", cnnSigamet)
        Try
            If cnnSigamet.State = ConnectionState.Open Then
                cnnSigamet.Close()
            Else
            End If
            cnnSigamet.Open()
            Dim dr As SqlDataReader = Valor.ExecuteReader
            While dr.Read
                _PuntosPermitidos = CType(dr("valor"), Integer)
            End While
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

    End Sub

    Private Sub ValidaComodato()

        Dim consulta As New SqlDataAdapter("SELECT SECUENCIA,SERIE,EQUIPO, TIPOPROPIEDAD  FROM vwSTValidaClienteEquipo WHERE  CLIENTE = " & lblContrato.Text, cnnSigamet)
        Dim dt As New DataTable("ValidaComodato")
        consulta.Fill(dt)
        If dt.Rows.Count > 0 Then
            If MessageBox.Show("El cliente " & lblContrato.Text & ", ya tiene un " & CType(dt.Rows(0).Item("Equipo"), String) & " , en tipo propiedad " & CType(dt.Rows(0).Item("TipoPropiedad"), String) & ",. ¿Desea agreagr otro equipo en comodato a este cliente?", "Servicios Técnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Else
                Me.Close()
            End If
        Else
        End If
    End Sub


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents ColPedidoReferencia As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColFCompromiso As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColUsuario As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColStatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColTipoServicio As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblSubtotal As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblNumPresupuesto As System.Windows.Forms.Label
    Friend WithEvents lblStatusPresupuesto As System.Windows.Forms.Label
    Friend WithEvents lblDescuento As System.Windows.Forms.Label
    Friend WithEvents lblPedido As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tmHora As System.Windows.Forms.Timer
    Friend WithEvents btnAceptar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnModificar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnPresupuesto As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnHorario As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents lvwHistorico As System.Windows.Forms.ListView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents lblcelula As System.Windows.Forms.Label
    Friend WithEvents txtTrabajoRealizado As System.Windows.Forms.TextBox
    Friend WithEvents txtObservaciones As System.Windows.Forms.TextBox
    Friend WithEvents cboTipoPedido As System.Windows.Forms.ComboBox
    Friend WithEvents cboTipoServicio As System.Windows.Forms.ComboBox
    Friend WithEvents dtpFCompromiso As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblNombreCliente As System.Windows.Forms.Label
    Friend WithEvents lblContrato As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents ClienteEquipo As STClienteEquipo.ClienteEquipo
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Protected WithEvents lstMaterial As System.Windows.Forms.ListBox
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents cboMaterial As System.Windows.Forms.ComboBox
    Friend WithEvents btnConfirmaDireccion As System.Windows.Forms.Button
    Friend WithEvents grbPago As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents lblPagosde As System.Windows.Forms.Label
    Friend WithEvents lblNumPagos As System.Windows.Forms.Label
    Friend WithEvents lblDias As System.Windows.Forms.Label
    Friend WithEvents cboTipoCredito As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblTecnico As System.Windows.Forms.Label
    Friend WithEvents txtTecnico As System.Windows.Forms.Label
    Friend WithEvents lblCamioneta As System.Windows.Forms.Label
    Friend WithEvents lblHoraatencion As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmServicios))
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.btnAceptar = New System.Windows.Forms.ToolBarButton()
        Me.btnModificar = New System.Windows.Forms.ToolBarButton()
        Me.btnPresupuesto = New System.Windows.Forms.ToolBarButton()
        Me.btnHorario = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lvwHistorico = New System.Windows.Forms.ListView()
        Me.ColPedidoReferencia = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColFCompromiso = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColUsuario = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColStatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColTipoServicio = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblSubtotal = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblNumPresupuesto = New System.Windows.Forms.Label()
        Me.lblStatusPresupuesto = New System.Windows.Forms.Label()
        Me.lblDescuento = New System.Windows.Forms.Label()
        Me.lblPedido = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.tmHora = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblHoraatencion = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.lblcelula = New System.Windows.Forms.Label()
        Me.txtTrabajoRealizado = New System.Windows.Forms.TextBox()
        Me.txtObservaciones = New System.Windows.Forms.TextBox()
        Me.cboTipoPedido = New System.Windows.Forms.ComboBox()
        Me.cboTipoServicio = New System.Windows.Forms.ComboBox()
        Me.dtpFCompromiso = New System.Windows.Forms.DateTimePicker()
        Me.lblNombreCliente = New System.Windows.Forms.Label()
        Me.lblContrato = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ClienteEquipo = New STClienteEquipo.ClienteEquipo()
        Me.grbPago = New System.Windows.Forms.GroupBox()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lstMaterial = New System.Windows.Forms.ListBox()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.cboMaterial = New System.Windows.Forms.ComboBox()
        Me.btnConfirmaDireccion = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lblPagosde = New System.Windows.Forms.Label()
        Me.lblNumPagos = New System.Windows.Forms.Label()
        Me.lblDias = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.cboTipoCredito = New System.Windows.Forms.ComboBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblTecnico = New System.Windows.Forms.Label()
        Me.txtTecnico = New System.Windows.Forms.Label()
        Me.lblCamioneta = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grbPago.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAceptar, Me.btnModificar, Me.btnPresupuesto, Me.btnHorario, Me.btnCerrar})
        Me.ToolBar1.ButtonSize = New System.Drawing.Size(70, 36)
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(928, 43)
        Me.ToolBar1.TabIndex = 0
        '
        'btnAceptar
        '
        Me.btnAceptar.ImageIndex = 0
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Text = "Aceptar"
        '
        'btnModificar
        '
        Me.btnModificar.ImageIndex = 1
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Text = "Modificar"
        '
        'btnPresupuesto
        '
        Me.btnPresupuesto.ImageIndex = 2
        Me.btnPresupuesto.Name = "btnPresupuesto"
        Me.btnPresupuesto.Text = "Presupuesto"
        '
        'btnHorario
        '
        Me.btnHorario.ImageIndex = 4
        Me.btnHorario.Name = "btnHorario"
        Me.btnHorario.Text = "Horario"
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 5
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Text = "Cerrar"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        Me.ImageList1.Images.SetKeyName(5, "")
        Me.ImageList1.Images.SetKeyName(6, "")
        Me.ImageList1.Images.SetKeyName(7, "")
        Me.ImageList1.Images.SetKeyName(8, "")
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.RoyalBlue
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(0, 448)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(480, 20)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Histórico del servicios técnicos"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox4
        '
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(488, 456)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(432, 120)
        Me.GroupBox4.TabIndex = 22
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Datos Presupuesto"
        '
        'lvwHistorico
        '
        Me.lvwHistorico.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvwHistorico.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColPedidoReferencia, Me.ColFCompromiso, Me.ColUsuario, Me.ColStatus, Me.ColTipoServicio})
        Me.lvwHistorico.FullRowSelect = True
        Me.lvwHistorico.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvwHistorico.Location = New System.Drawing.Point(0, 472)
        Me.lvwHistorico.Name = "lvwHistorico"
        Me.lvwHistorico.Size = New System.Drawing.Size(480, 104)
        Me.lvwHistorico.SmallImageList = Me.ImageList1
        Me.lvwHistorico.TabIndex = 1
        Me.lvwHistorico.UseCompatibleStateImageBehavior = False
        Me.lvwHistorico.View = System.Windows.Forms.View.Details
        '
        'ColPedidoReferencia
        '
        Me.ColPedidoReferencia.Text = "Pedido Referencia"
        Me.ColPedidoReferencia.Width = 100
        '
        'ColFCompromiso
        '
        Me.ColFCompromiso.Text = "FCompromiso"
        Me.ColFCompromiso.Width = 75
        '
        'ColUsuario
        '
        Me.ColUsuario.Text = "Usuario"
        Me.ColUsuario.Width = 65
        '
        'ColStatus
        '
        Me.ColStatus.Text = "Status"
        Me.ColStatus.Width = 73
        '
        'ColTipoServicio
        '
        Me.ColTipoServicio.Text = "TipoServicio"
        Me.ColTipoServicio.Width = 132
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(728, 544)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 321
        Me.Label2.Text = " Total:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(728, 480)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 13)
        Me.Label7.TabIndex = 320
        Me.Label7.Text = "SubTotal:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSubtotal
        '
        Me.lblSubtotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSubtotal.BackColor = System.Drawing.SystemColors.Control
        Me.lblSubtotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSubtotal.Location = New System.Drawing.Point(800, 480)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(112, 21)
        Me.lblSubtotal.TabIndex = 319
        Me.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(728, 512)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(62, 13)
        Me.Label12.TabIndex = 318
        Me.Label12.Text = "Descuento:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(496, 544)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(99, 13)
        Me.Label11.TabIndex = 317
        Me.Label11.Text = "StatusPresupuesto:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label30
        '
        Me.Label30.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(496, 480)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(97, 13)
        Me.Label30.TabIndex = 316
        Me.Label30.Text = "Num. Presupuesto:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotal.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(800, 544)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(112, 21)
        Me.lblTotal.TabIndex = 315
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNumPresupuesto
        '
        Me.lblNumPresupuesto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNumPresupuesto.BackColor = System.Drawing.SystemColors.Control
        Me.lblNumPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumPresupuesto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumPresupuesto.Location = New System.Drawing.Point(600, 480)
        Me.lblNumPresupuesto.Name = "lblNumPresupuesto"
        Me.lblNumPresupuesto.Size = New System.Drawing.Size(112, 21)
        Me.lblNumPresupuesto.TabIndex = 314
        Me.lblNumPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatusPresupuesto
        '
        Me.lblStatusPresupuesto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatusPresupuesto.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatusPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatusPresupuesto.Location = New System.Drawing.Point(600, 544)
        Me.lblStatusPresupuesto.Name = "lblStatusPresupuesto"
        Me.lblStatusPresupuesto.Size = New System.Drawing.Size(112, 21)
        Me.lblStatusPresupuesto.TabIndex = 313
        Me.lblStatusPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDescuento
        '
        Me.lblDescuento.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDescuento.BackColor = System.Drawing.SystemColors.Control
        Me.lblDescuento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDescuento.Location = New System.Drawing.Point(800, 512)
        Me.lblDescuento.Name = "lblDescuento"
        Me.lblDescuento.Size = New System.Drawing.Size(112, 21)
        Me.lblDescuento.TabIndex = 312
        Me.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPedido
        '
        Me.lblPedido.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPedido.BackColor = System.Drawing.SystemColors.Control
        Me.lblPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPedido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPedido.Location = New System.Drawing.Point(600, 512)
        Me.lblPedido.Name = "lblPedido"
        Me.lblPedido.Size = New System.Drawing.Size(112, 21)
        Me.lblPedido.TabIndex = 311
        Me.lblPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(496, 512)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 13)
        Me.Label13.TabIndex = 310
        Me.Label13.Text = "Pedido:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblHoraatencion)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label51)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblRuta)
        Me.GroupBox1.Controls.Add(Me.lblcelula)
        Me.GroupBox1.Controls.Add(Me.txtTrabajoRealizado)
        Me.GroupBox1.Controls.Add(Me.txtObservaciones)
        Me.GroupBox1.Controls.Add(Me.cboTipoPedido)
        Me.GroupBox1.Controls.Add(Me.cboTipoServicio)
        Me.GroupBox1.Controls.Add(Me.dtpFCompromiso)
        Me.GroupBox1.Controls.Add(Me.lblNombreCliente)
        Me.GroupBox1.Controls.Add(Me.lblContrato)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 48)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(488, 240)
        Me.GroupBox1.TabIndex = 322
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Datos del Servicio Técnico"
        '
        'lblHoraatencion
        '
        Me.lblHoraatencion.BackColor = System.Drawing.SystemColors.ControlLight
        Me.lblHoraatencion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHoraatencion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHoraatencion.Location = New System.Drawing.Point(304, 116)
        Me.lblHoraatencion.Name = "lblHoraatencion"
        Me.lblHoraatencion.Size = New System.Drawing.Size(160, 21)
        Me.lblHoraatencion.TabIndex = 301
        Me.lblHoraatencion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(352, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 300
        Me.Label9.Text = "Ruta :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(240, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 13)
        Me.Label8.TabIndex = 298
        Me.Label8.Text = "Celula :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(248, 148)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 13)
        Me.Label5.TabIndex = 296
        Me.Label5.Text = "Trabajo Realizado"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(4, 148)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(110, 13)
        Me.Label14.TabIndex = 295
        Me.Label14.Text = "Trabajo Solicitado"
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(4, 116)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(100, 13)
        Me.Label51.TabIndex = 293
        Me.Label51.Text = "Forma de Cargo:"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(4, 84)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(86, 13)
        Me.Label26.TabIndex = 292
        Me.Label26.Text = "TipoServicio :"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(4, 56)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(89, 13)
        Me.Label24.TabIndex = 291
        Me.Label24.Text = "FCompromiso :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 288
        Me.Label6.Text = "Cliente :"
        '
        'lblRuta
        '
        Me.lblRuta.BackColor = System.Drawing.SystemColors.ControlLight
        Me.lblRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuta.Location = New System.Drawing.Point(400, 52)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(64, 21)
        Me.lblRuta.TabIndex = 299
        Me.lblRuta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblcelula
        '
        Me.lblcelula.BackColor = System.Drawing.SystemColors.ControlLight
        Me.lblcelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblcelula.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcelula.Location = New System.Drawing.Point(288, 52)
        Me.lblcelula.Name = "lblcelula"
        Me.lblcelula.Size = New System.Drawing.Size(48, 21)
        Me.lblcelula.TabIndex = 297
        Me.lblcelula.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTrabajoRealizado
        '
        Me.txtTrabajoRealizado.BackColor = System.Drawing.SystemColors.Control
        Me.txtTrabajoRealizado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrabajoRealizado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrabajoRealizado.Location = New System.Drawing.Point(248, 164)
        Me.txtTrabajoRealizado.Multiline = True
        Me.txtTrabajoRealizado.Name = "txtTrabajoRealizado"
        Me.txtTrabajoRealizado.ReadOnly = True
        Me.txtTrabajoRealizado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTrabajoRealizado.Size = New System.Drawing.Size(235, 64)
        Me.txtTrabajoRealizado.TabIndex = 294
        '
        'txtObservaciones
        '
        Me.txtObservaciones.BackColor = System.Drawing.Color.White
        Me.txtObservaciones.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObservaciones.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservaciones.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtObservaciones.Location = New System.Drawing.Point(4, 164)
        Me.txtObservaciones.Multiline = True
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservaciones.Size = New System.Drawing.Size(235, 64)
        Me.txtObservaciones.TabIndex = 287
        '
        'cboTipoPedido
        '
        Me.cboTipoPedido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoPedido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTipoPedido.Items.AddRange(New Object() {"ACTIVO", "PROGRAMADO", "EN SERVICIO", "ATENDIDO", "CANCELADO"})
        Me.cboTipoPedido.Location = New System.Drawing.Point(112, 116)
        Me.cboTipoPedido.Name = "cboTipoPedido"
        Me.cboTipoPedido.Size = New System.Drawing.Size(128, 21)
        Me.cboTipoPedido.TabIndex = 286
        '
        'cboTipoServicio
        '
        Me.cboTipoServicio.DisplayMember = "Descripcion"
        Me.cboTipoServicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoServicio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTipoServicio.ItemHeight = 13
        Me.cboTipoServicio.Location = New System.Drawing.Point(112, 84)
        Me.cboTipoServicio.Name = "cboTipoServicio"
        Me.cboTipoServicio.Size = New System.Drawing.Size(352, 21)
        Me.cboTipoServicio.TabIndex = 285
        Me.cboTipoServicio.ValueMember = "PrioridadPedido"
        '
        'dtpFCompromiso
        '
        Me.dtpFCompromiso.CalendarTitleForeColor = System.Drawing.SystemColors.Window
        Me.dtpFCompromiso.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFCompromiso.Location = New System.Drawing.Point(112, 52)
        Me.dtpFCompromiso.Name = "dtpFCompromiso"
        Me.dtpFCompromiso.Size = New System.Drawing.Size(120, 20)
        Me.dtpFCompromiso.TabIndex = 284
        '
        'lblNombreCliente
        '
        Me.lblNombreCliente.BackColor = System.Drawing.SystemColors.Control
        Me.lblNombreCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNombreCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombreCliente.Location = New System.Drawing.Point(232, 20)
        Me.lblNombreCliente.Name = "lblNombreCliente"
        Me.lblNombreCliente.Size = New System.Drawing.Size(232, 21)
        Me.lblNombreCliente.TabIndex = 290
        Me.lblNombreCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContrato
        '
        Me.lblContrato.BackColor = System.Drawing.SystemColors.Control
        Me.lblContrato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblContrato.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContrato.Location = New System.Drawing.Point(112, 20)
        Me.lblContrato.Name = "lblContrato"
        Me.lblContrato.Size = New System.Drawing.Size(112, 21)
        Me.lblContrato.TabIndex = 289
        Me.lblContrato.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(248, 120)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(47, 13)
        Me.Label16.TabIndex = 297
        Me.Label16.Text = "Horario :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ClienteEquipo)
        Me.GroupBox2.Location = New System.Drawing.Point(504, 144)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(416, 144)
        Me.GroupBox2.TabIndex = 323
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Comodato"
        '
        'ClienteEquipo
        '
        Me.ClienteEquipo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ClienteEquipo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClienteEquipo.Location = New System.Drawing.Point(8, 16)
        Me.ClienteEquipo.Name = "ClienteEquipo"
        Me.ClienteEquipo.Size = New System.Drawing.Size(400, 120)
        Me.ClienteEquipo.TabIndex = 295
        '
        'grbPago
        '
        Me.grbPago.Controls.Add(Me.txtCantidad)
        Me.grbPago.Controls.Add(Me.Label28)
        Me.grbPago.Controls.Add(Me.Label21)
        Me.grbPago.Controls.Add(Me.lstMaterial)
        Me.grbPago.Controls.Add(Me.btnEliminar)
        Me.grbPago.Controls.Add(Me.btnAgregar)
        Me.grbPago.Controls.Add(Me.cboMaterial)
        Me.grbPago.Controls.Add(Me.btnConfirmaDireccion)
        Me.grbPago.Location = New System.Drawing.Point(8, 296)
        Me.grbPago.Name = "grbPago"
        Me.grbPago.Size = New System.Drawing.Size(912, 104)
        Me.grbPago.TabIndex = 0
        Me.grbPago.TabStop = False
        Me.grbPago.Text = "Detalle del Cargo"
        '
        'txtCantidad
        '
        Me.txtCantidad.Location = New System.Drawing.Point(8, 72)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(32, 20)
        Me.txtCantidad.TabIndex = 3
        Me.txtCantidad.Text = "1"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(504, 16)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(182, 13)
        Me.Label28.TabIndex = 340
        Me.Label28.Text = "Lista de Material Seleccionado"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(8, 48)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(98, 13)
        Me.Label21.TabIndex = 339
        Me.Label21.Text = "Lista de Precios"
        '
        'lstMaterial
        '
        Me.lstMaterial.BackColor = System.Drawing.SystemColors.ControlLight
        Me.lstMaterial.ColumnWidth = 2
        Me.lstMaterial.Location = New System.Drawing.Point(504, 40)
        Me.lstMaterial.Name = "lstMaterial"
        Me.lstMaterial.Size = New System.Drawing.Size(400, 56)
        Me.lstMaterial.TabIndex = 337
        '
        'btnEliminar
        '
        Me.btnEliminar.Location = New System.Drawing.Point(440, 72)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(48, 24)
        Me.btnEliminar.TabIndex = 336
        Me.btnEliminar.Text = "<<"
        '
        'btnAgregar
        '
        Me.btnAgregar.Location = New System.Drawing.Point(440, 40)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(48, 24)
        Me.btnAgregar.TabIndex = 334
        Me.btnAgregar.Text = ">>"
        '
        'cboMaterial
        '
        Me.cboMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaterial.Location = New System.Drawing.Point(48, 72)
        Me.cboMaterial.Name = "cboMaterial"
        Me.cboMaterial.Size = New System.Drawing.Size(376, 21)
        Me.cboMaterial.TabIndex = 333
        '
        'btnConfirmaDireccion
        '
        Me.btnConfirmaDireccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfirmaDireccion.Image = CType(resources.GetObject("btnConfirmaDireccion.Image"), System.Drawing.Image)
        Me.btnConfirmaDireccion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfirmaDireccion.Location = New System.Drawing.Point(8, 16)
        Me.btnConfirmaDireccion.Name = "btnConfirmaDireccion"
        Me.btnConfirmaDireccion.Size = New System.Drawing.Size(144, 24)
        Me.btnConfirmaDireccion.TabIndex = 332
        Me.btnConfirmaDireccion.Text = "Confirma Dirección"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.Label25)
        Me.GroupBox3.Controls.Add(Me.Label27)
        Me.GroupBox3.Controls.Add(Me.lblPagosde)
        Me.GroupBox3.Controls.Add(Me.lblNumPagos)
        Me.GroupBox3.Controls.Add(Me.lblDias)
        Me.GroupBox3.Controls.Add(Me.Label33)
        Me.GroupBox3.Controls.Add(Me.cboTipoCredito)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 400)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(912, 48)
        Me.GroupBox3.TabIndex = 325
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Datos del crédito"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(816, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 358
        Me.Label4.Text = "Días"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(718, 20)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(40, 13)
        Me.Label18.TabIndex = 353
        Me.Label18.Text = "Cada:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(542, 20)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(64, 13)
        Me.Label19.TabIndex = 352
        Me.Label19.Text = "Pagos de:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(398, 20)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(75, 13)
        Me.Label25.TabIndex = 351
        Me.Label25.Text = "Núm Pagos:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(606, 14)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(24, 24)
        Me.Label27.TabIndex = 357
        Me.Label27.Text = "$"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPagosde
        '
        Me.lblPagosde.BackColor = System.Drawing.SystemColors.Control
        Me.lblPagosde.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPagosde.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPagosde.ForeColor = System.Drawing.Color.Gray
        Me.lblPagosde.Location = New System.Drawing.Point(638, 16)
        Me.lblPagosde.Name = "lblPagosde"
        Me.lblPagosde.Size = New System.Drawing.Size(72, 21)
        Me.lblPagosde.TabIndex = 356
        Me.lblPagosde.Text = "0"
        Me.lblPagosde.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNumPagos
        '
        Me.lblNumPagos.BackColor = System.Drawing.SystemColors.Control
        Me.lblNumPagos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumPagos.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumPagos.ForeColor = System.Drawing.Color.Gray
        Me.lblNumPagos.Location = New System.Drawing.Point(494, 16)
        Me.lblNumPagos.Name = "lblNumPagos"
        Me.lblNumPagos.Size = New System.Drawing.Size(32, 21)
        Me.lblNumPagos.TabIndex = 355
        Me.lblNumPagos.Text = "0"
        Me.lblNumPagos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDias
        '
        Me.lblDias.BackColor = System.Drawing.SystemColors.Control
        Me.lblDias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDias.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDias.ForeColor = System.Drawing.Color.Gray
        Me.lblDias.Location = New System.Drawing.Point(768, 16)
        Me.lblDias.Name = "lblDias"
        Me.lblDias.Size = New System.Drawing.Size(32, 21)
        Me.lblDias.TabIndex = 354
        Me.lblDias.Text = "0"
        Me.lblDias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(16, 20)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(111, 13)
        Me.Label33.TabIndex = 350
        Me.Label33.Text = "Número de Pagos:"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboTipoCredito
        '
        Me.cboTipoCredito.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTipoCredito.ItemHeight = 13
        Me.cboTipoCredito.Items.AddRange(New Object() {"Sin Crédito"})
        Me.cboTipoCredito.Location = New System.Drawing.Point(136, 16)
        Me.cboTipoCredito.Name = "cboTipoCredito"
        Me.cboTipoCredito.Size = New System.Drawing.Size(184, 21)
        Me.cboTipoCredito.TabIndex = 349
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.lblTecnico)
        Me.GroupBox5.Controls.Add(Me.txtTecnico)
        Me.GroupBox5.Controls.Add(Me.lblCamioneta)
        Me.GroupBox5.Location = New System.Drawing.Point(504, 56)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(416, 80)
        Me.GroupBox5.TabIndex = 326
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Datos del Técnico"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 13)
        Me.Label10.TabIndex = 297
        Me.Label10.Text = "Camioneta:"
        '
        'lblTecnico
        '
        Me.lblTecnico.AutoSize = True
        Me.lblTecnico.Location = New System.Drawing.Point(8, 54)
        Me.lblTecnico.Name = "lblTecnico"
        Me.lblTecnico.Size = New System.Drawing.Size(52, 13)
        Me.lblTecnico.TabIndex = 295
        Me.lblTecnico.Text = "Técnico :"
        '
        'txtTecnico
        '
        Me.txtTecnico.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtTecnico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtTecnico.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTecnico.Location = New System.Drawing.Point(72, 50)
        Me.txtTecnico.Name = "txtTecnico"
        Me.txtTecnico.Size = New System.Drawing.Size(288, 21)
        Me.txtTecnico.TabIndex = 296
        Me.txtTecnico.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCamioneta
        '
        Me.lblCamioneta.BackColor = System.Drawing.SystemColors.ControlLight
        Me.lblCamioneta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCamioneta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCamioneta.Location = New System.Drawing.Point(72, 20)
        Me.lblCamioneta.Name = "lblCamioneta"
        Me.lblCamioneta.Size = New System.Drawing.Size(120, 21)
        Me.lblCamioneta.TabIndex = 298
        Me.lblCamioneta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmServicios
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(928, 584)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.grbPago)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lblSubtotal)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.lblNumPresupuesto)
        Me.Controls.Add(Me.lblStatusPresupuesto)
        Me.Controls.Add(Me.lblDescuento)
        Me.Controls.Add(Me.lblPedido)
        Me.Controls.Add(Me.lvwHistorico)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ToolBar1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmServicios"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Servicios Técnicos   "
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.grbPago.ResumeLayout(False)
        Me.grbPago.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region



    Private Sub btnConfirmaDirección_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Cursor = Cursors.WaitCursor
        Dim ConfirmaDireccion As New frmConfirmaDireccion(CType(lblContrato.Text, Integer))
        ConfirmaDireccion.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub frmServicioST_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not cnnSigamet Is Nothing Then
            If cnnSigamet.State = ConnectionState.Open Then
                cnnSigamet.Close()
            End If
        End If
        TablaParametro()
        LlenaCombos()
        Dim ActivaPuntos As Boolean
        Dim _MañanaEsDiaFestivo As Boolean
        Dim _FechaCorte As Date
        Dim _FechaCorteFin As Date
        'Dim oConfig As New SigaMetClasses.cConfig(11)
        Dim oConfig As New SigaMetClasses.cConfig(11, GLOBAL_Corporativo, GLOBAL_Sucursal)
        Dim _FechaDestino As Date = Nothing
        Dim _FechaExtraordinaria As Date = _
            SigaMetClasses.CalculaFechaCardinal(Now.Year, _
            CType(Now.Month, SigaMetClasses.Enumeradores.enumMesAño), _
            SigaMetClasses.Enumeradores.enumDiaSemana.Domingo, _
            SigaMetClasses.Enumeradores.enumCardinalidad.Segundo)
        'lblStatusServicioTecnico.Visible = False
        'lblPuntos.Visible = False
        'lblTotalEjecutivas.Visible = False
        'lblAñoPed.Visible = False
        'lblAñoFolioPresupuesto.Enabled = False
        CargaDatosHistoricoS()
        'LlenaCombos()
        'LLenaPrecio()
        _FPedido = CType(Now.ToShortDateString, Date)

        tmHora.Enabled = True
        txtObservaciones.Select()
        DatosCargados = True
        btnPresupuesto.Enabled = False
        ClienteEquipo.Enabled = False
        'Dim Puntos As New SigaMetClasses.cConfig(11)
        Dim Puntos As New SigaMetClasses.cConfig(11, GLOBAL_Corporativo, GLOBAL_Sucursal)
        ActivaPuntos = CType(Puntos.Parametros("ActivarLimitePuntos"), Boolean)
        If ActivaPuntos = True Then
            Do While SumaPuntos(dtpFCompromiso.Value.Date, CType(lblcelula.Text, Byte)) >= _PuntosPermitidos
                Dim Fecha As String
                Fecha = CType(dtpFCompromiso.Value, String)
                MessageBox.Show("Usted a llegado al limite de servicios técnicos del día " & Fecha & ", se le asignara una nueva fecha compromiso.", "Mensaje de Servicios Técnicos")
                dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(1)
                'LUSATE SE MODIFICÓ PARA GOAX
                'dtpFCompromiso.MinDate = dtpFCompromiso.Value
                If dtpFCompromiso.Value.DayOfWeek = DayOfWeek.Sunday Then
                    If dtpFCompromiso.Value.Date <> _
                                            SigaMetClasses.CalculaFechaCardinal(dtpFCompromiso.Value.AddDays(1).Year, CType(dtpFCompromiso.Value.AddDays(1).Month, SigaMetClasses.Enumeradores.enumMesAño), SigaMetClasses.Enumeradores.enumDiaSemana.Domingo, SigaMetClasses.Enumeradores.enumCardinalidad.Segundo) Then
                        dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(1)
                        'LUSATE SE MODIFICÓ PARA GOAX
                        'dtpFCompromiso.MinDate = dtpFCompromiso.Value
                    End If
                Else
                    Try
                        ST_HoraCorte = CType(oConfig.Parametros("HoraCorteEntreSemana"), String)
                        ST_HoraCorteFin = CType(oConfig.Parametros("horacortefinsemana"), String)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    Finally
                        oConfig.Dispose()

                    End Try

                    _FechaCorte = CType((Now.Date.ToShortDateString & " " & ST_HoraCorte), Date)
                    _FechaCorteFin = CType(Now.Date.ToShortDateString & " " & ST_HoraCorteFin, Date)

                    Dim strQuery As String = _
                    "SELECT Año, Mes, Dia FROM Festivo WHERE Tipo = 'FESTIVO'"
                    Dim da As New SqlDataAdapter(strQuery, cnnSigamet)
                    Dim dt As New DataTable("Festivo")
                    Try
                        da.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            Dim dr As DataRow
                            For Each dr In dt.Rows
                                If CType(dr("Dia"), Integer) = dtpFCompromiso.Value.Day And CType(dr("Mes"), Integer) = Now.Date.AddDays(1).Month Then
                                    _MañanaEsDiaFestivo = True
                                    Exit For
                                End If
                            Next
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    Finally
                        da.Dispose()
                    End Try

                    'If Now < _FechaCorte Then
                    '    'Antes del corte
                    '    If Not _MañanaEsDiaFestivo Then
                    '        Select Case Now.DayOfWeek
                    '            Case DayOfWeek.Saturday
                    '                If Now.Date.AddDays(1) = _FechaExtraordinaria Then
                    '                    dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(1)
                    '                Else
                    '                    'dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(2)
                    '                End If

                    '            Case Is = DayOfWeek.Sunday
                    '                dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(2)
                    '            Case Else
                    '                'dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(1)
                    '        End Select
                    '    Else
                    '        Select Case Now.DayOfWeek
                    '            Case DayOfWeek.Friday
                    '                If Now.Date.AddDays(2) = _FechaExtraordinaria Then
                    '                    dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(2)
                    '                Else
                    '                    dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(3)
                    '                End If
                    '            Case Else
                    '                'dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(2)

                    '        End Select

                    '    End If
                    'Else
                    '    'Despues del corte
                    '    If Not _MañanaEsDiaFestivo Then
                    '        Select Case Now.DayOfWeek
                    '            Case Is = DayOfWeek.Friday
                    '                'If Now.Date.AddDays(2) = _FechaExtraordinaria Then
                    '                '    dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(2)
                    '                'Else
                    '                '    dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(3)
                    '                'End If

                    '            Case Is = DayOfWeek.Saturday
                    '                If Now.Date.AddDays(1) = _FechaExtraordinaria Then
                    '                    dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(2)
                    '                Else
                    '                    dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(3)
                    '                End If

                    '            Case Else
                    '                'dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(2)
                    '        End Select
                    '    Else
                    '        Select Case Now.DayOfWeek
                    '            Case DayOfWeek.Thursday
                    '                If Now.Date.AddDays(3) = _FechaExtraordinaria Then
                    '                    dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(3)
                    '                Else
                    '                    dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(4)
                    '                End If

                    '            Case DayOfWeek.Friday
                    '                If Now.Date.AddDays(2) = _FechaExtraordinaria Then
                    '                    dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(3)
                    '                Else
                    '                    dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(4)
                    '                End If
                    '            Case Else
                    '                dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(3)
                    '        End Select

                    '    End If

                    'End If
                End If
            Loop

        End If

        If oSeguridad.TieneAcceso("MODIFICA FECHA") Then
            dtpFCompromiso.Value = dtpFCompromiso.Value
            'LUSATE SE MODIFICÓ PARA GOAX
            'dtpFCompromiso.MinDate = dtpFCompromiso.Value.AddMonths(-1)
        Else
            dtpFCompromiso.Value = _dtpFCompromiso.Value
            'LUSATE SE MODIFICÓ PARA GOAX
            'dtpFCompromiso.MinDate = dtpFCompromiso.Value
        End If

        btnConfirmaDireccion.Enabled = False
        txtCantidad.Enabled = False
        cboTipoCredito.Enabled = False
        lblNumPagos.Enabled = False
        lblPagosde.Enabled = False
        lblDias.Enabled = False
        cboMaterial.Enabled = False
        btnAgregar.Enabled = False
        btnEliminar.Enabled = False
        lstMaterial.Enabled = True
        Label21.Enabled = False

        StatusPresupuesto()
    End Sub

    Private Sub PuntosTipoServicio()
        Dim query As String = "select puntos from tiposervicio where tiposervicio = " & TipoServicio
        Dim daTipoServicio As New SqlDataAdapter(query, cnnSigamet)
        Dim dtTiposervicio As New DataTable("TipoServicio")
        daTipoServicio.Fill(dtTiposervicio)
        If dtTiposervicio.Rows.Count > 0 Then
            _PuntosTipoServicio = CType(dtTiposervicio.Rows(0).Item("Puntos"), Integer)
        Else
            _PuntosTipoServicio = 0
        End If

    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Text
            Case "Aceptar"
                SumaPuntos(dtpFCompromiso.Value, CType(lblcelula.Text, Byte))
                If Puntos >= _PuntosPermitidos Then
                    MessageBox.Show("La fecha seleccionada esta llena, debe de seleccionar otra fecha.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                Else
                End If
                If txtObservaciones.Text = "" Then
                    MessageBox.Show("Usted debe anotar las observaciones del cliente.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    Dim TipoPedido As Integer
                    TipoPedido = CType(cboTipoPedido.SelectedValue, Integer)
                    If TipoPedido <> 8 Then
                        If _Folio = Nothing Then
                            MessageBox.Show("Usted debe confirmar la dirección de la instalación.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        Else
                            If TipoPedido = 9 Then
                                If MessageBox.Show("¿Los datos estan correctos?", "Servicios Técnicos", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                                    TipoServicio = CType(cboTipoServicio.SelectedValue, Integer)
                                    PuntosTipoServicio()
                                    Dim TotalPuntos As Integer
                                    TotalPuntos = _PuntosTipoServicio + Puntos
                                    If TotalPuntos > _PuntosPermitidos Then
                                        MessageBox.Show("Usted no puede ingresar un servicio de mas de 3 puntos, Póngase en contacto con Célula 7", "Mensaje de Servicios Técnicos")
                                        Exit Sub
                                    Else
                                    End If

                                    Dim oEquipo As STClienteEquipo.sClienteEquipoItem
                                    Dim oDatos As New STClienteEquipo.cClienteEquipo()
                                    Dim conn As SqlConnection
                                    Try

                                        conn = SigaMetClasses.DataLayer.Conexion
                                        conn.Open()
                                        Dim cmd As New SqlCommand()
                                        cmd.Connection = conn
                                        cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar).Value = txtObservaciones.Text
                                        cmd.Parameters.Add("@TipoPedido", SqlDbType.Int).Value = cboTipoPedido.SelectedValue
                                        cmd.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value

                                        If lblHoraatencion.Text = "" Then
                                            cmd.Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                                        Else
                                            cmd.Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = CType(lblHoraatencion.Text, DateTime)
                                        End If

                                        cmd.Parameters.Add("@Cliente", SqlDbType.Int).Value = lblContrato.Text
                                        cmd.Parameters.Add("@Celula", SqlDbType.Int).Value = lblcelula.Text
                                        cmd.Parameters.Add("@Ruta", SqlDbType.Int).Value = lblRuta.Text
                                        cmd.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                                        cmd.Parameters.Add("@TipoServicio", SqlDbType.Int).Value = cboTipoServicio.SelectedValue

                                        cmd.Parameters.Add("@NumExterior", SqlDbType.Char).Value = NumExterior
                                        cmd.Parameters.Add("@NumInterior", SqlDbType.Char).Value = NumInterior
                                        cmd.Parameters.Add("@Calle", SqlDbType.Int).Value = Calle
                                        cmd.Parameters.Add("@Colonia", SqlDbType.Int).Value = Colonia

                                        'For Each oEquipo In Me.ClienteEquipo.Equipos
                                        'SQLComandoTransaccion.Parameters.Add("@Equipo", SqlDbType.SmallInt).Value = oEquipo.Equipo
                                        'SQLComandoTransaccion.Parameters.Add("@TipoPropiedad", SqlDbType.TinyInt).Value = 2
                                        'SQLComandoTransaccion.Parameters.Add("@Serie", SqlDbType.Char).Value = oEquipo.Serie
                                        'SQLComandoTransaccion.Parameters.Add("@FFabricacion", SqlDbType.DateTime).Value = oEquipo.FFabricacion
                                        'SQLComandoTransaccion.Parameters.Add("@Precio", SqlDbType.Money).Value = oEquipo.Precio
                                        'SQLComandoTransaccion.Parameters.Add("@FInicioComodato", SqlDbType.DateTime).Value = oEquipo.FInicioComodato
                                        'SQLComandoTransaccion.Parameters.Add("@FFinComodato", SqlDbType.DateTime).Value = oEquipo.FFinComodato
                                        'SQLComandoTransaccion.Parameters.Add("@Status", SqlDbType.Char).Value = oEquipo.Status
                                        'SQLComandoTransaccion.Parameters.Add("@Consumo", SqlDbType.Int).Value = oEquipo.Consumo

                                        'Next

                                        'If conexiontransaccion.State = ConnectionState.Open Then
                                        '    conexiontransaccion.Close()
                                        'Else
                                        'End If
                                        cmd.CommandType = CommandType.StoredProcedure
                                        cmd.CommandText = "spSTPedidoServicioTecnicoAltaNuevo"
                                        Dim reader As SqlDataReader = cmd.ExecuteReader()
                                        Dim pedido As Int32
                                        Dim celula As Int32
                                        Dim anio As Int32
                                        While (reader.Read())
                                            celula = Convert.ToInt32(reader("Celula").ToString())
                                            anio = Convert.ToInt32(reader("AñoPed").ToString())
                                            pedido = Convert.ToInt32(reader("Pedido").ToString())
                                        End While
                                        If conn.State = ConnectionState.Open Then
                                            conn.Close()
                                        Else
                                        End If

                                        'Componente(STClienteEquipo)
                                        'SE AGREGA PARA COMPLETAR EL COMODATO
                                        'Dim oEquipo As STClienteEquipo.sClienteEquipoItem
                                        Dim oClienteEquipo As New STClienteEquipo.cClienteEquipo()

                                        For Each oEquipo In Me.ClienteEquipo.Equipos
                                            oClienteEquipo.Alta(CType(Me.lblContrato.Text, Integer), oEquipo.Equipo, 2, oEquipo.Precio, oEquipo.FFabricacion, oEquipo.Serie, oEquipo.FInicioComodato, oEquipo.FFinComodato, oEquipo.Status, oEquipo.Consumo, _Usuario, pedido, celula, anio)
                                        Next

                                    Catch ex As Exception
                                        MessageBox.Show(ex.Message)
                                    Finally

                                        Me.Close()
                                    End Try
                                Else
                                End If

                            Else
                                If lblTotal.Text = "" Then
                                    MessageBox.Show("Usted debe de seleccionar un material.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                Else
                                    'PERMITE GUARDAR LOS DATOS DE UN SERVICIO TECNICO CON CARGO
                                    If TipoPedido = 7 Then
                                        If MessageBox.Show("¿Los datos estan correctos?", "Servicios Técnicos", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                            '        Dim strQuery As String = _
                                            '"SELECT TotalPuntos FROM vwSTSumaPuntos " & _
                                            '"where producto = 4 and fcompromiso = '" & dtpFCompromiso.Value.ToShortDateString & "' and celula = " & lblcelula.Text

                                            '        Dim daSumaPuntos As New SqlDataAdapter(strQuery, cnnSigamet)
                                            '        Dim dtSumaPuntos As New DataTable("Suma")
                                            '        daSumaPuntos.Fill(dtSumaPuntos)
                                            '        If dtSumaPuntos.Rows.Count > 0 Then
                                            '            Puntos = CType(dtSumaPuntos.Rows(0).Item("totalpuntos"), Integer)
                                            '        Else
                                            '            Puntos = CType(0, Integer)
                                            '        End If
                                            'Puntos = CType(dtSumaPuntos.Rows(0).Item("totalpuntos"), Integer)
                                            TipoServicio = CType(cboTipoServicio.SelectedValue, Integer)
                                            PuntosTipoServicio()
                                            Dim TotalPuntos As Integer
                                            TotalPuntos = _PuntosTipoServicio + Puntos
                                            If TotalPuntos > _PuntosPermitidos Then
                                                MessageBox.Show("Usted no puede ingresar un servicio de mas de 3 puntos, Póngase en contacto con Célula 7", "Mensaje de Servicios Técnicos")
                                                Exit Sub
                                            Else
                                            End If

                                            Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                                            ConexionTransaccion.Open()
                                            Dim SQLComandoTransaccion As New SqlCommand()
                                            Dim SQLTransaccion As SqlTransaction
                                            SQLComandoTransaccion.Parameters.Add("@Observaciones", SqlDbType.VarChar).Value = txtObservaciones.Text
                                            SQLComandoTransaccion.Parameters.Add("@TipoPedido", SqlDbType.Int).Value = cboTipoPedido.SelectedValue
                                            SQLComandoTransaccion.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value

                                            If lblHoraatencion.Text = "" Then
                                                SQLComandoTransaccion.Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                                            Else
                                                SQLComandoTransaccion.Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = CType(lblHoraatencion.Text, DateTime)
                                            End If

                                            SQLComandoTransaccion.Parameters.Add("@Cliente", SqlDbType.Int).Value = lblContrato.Text
                                            SQLComandoTransaccion.Parameters.Add("@Celula", SqlDbType.Int).Value = lblcelula.Text
                                            SQLComandoTransaccion.Parameters.Add("@Ruta", SqlDbType.Int).Value = lblRuta.Text
                                            SQLComandoTransaccion.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                                            SQLComandoTransaccion.Parameters.Add("@TipoServicio", SqlDbType.Int).Value = cboTipoServicio.SelectedValue

                                            SQLComandoTransaccion.Parameters.Add("@Total", SqlDbType.Money).Value = lblTotal.Text
                                            SQLComandoTransaccion.Parameters.Add("@Material", SqlDbType.Int).Value = cboMaterial.SelectedValue
                                            SQLComandoTransaccion.Parameters.Add("@Cantidad", SqlDbType.Int).Value = txtCantidad.Text
                                            SQLComandoTransaccion.Parameters.Add("@NumExterior", SqlDbType.Char).Value = NumExterior
                                            SQLComandoTransaccion.Parameters.Add("@NumInterior", SqlDbType.Char).Value = NumInterior
                                            SQLComandoTransaccion.Parameters.Add("@Calle", SqlDbType.Int).Value = Calle
                                            SQLComandoTransaccion.Parameters.Add("@Colonia", SqlDbType.Int).Value = Colonia

                                            SQLComandoTransaccion.Parameters.Add("@Equipo", SqlDbType.SmallInt).Value = 0
                                            SQLComandoTransaccion.Parameters.Add("@TipoPropiedad", SqlDbType.TinyInt).Value = 0
                                            SQLComandoTransaccion.Parameters.Add("@Serie", SqlDbType.Char).Value = ""
                                            SQLComandoTransaccion.Parameters.Add("@FFabricacion", SqlDbType.DateTime).Value = Nothing
                                            SQLComandoTransaccion.Parameters.Add("@Precio", SqlDbType.Money).Value = 0
                                            SQLComandoTransaccion.Parameters.Add("@FInicioComodato", SqlDbType.DateTime).Value = Nothing
                                            SQLComandoTransaccion.Parameters.Add("@FFinComodato", SqlDbType.DateTime).Value = Nothing
                                            SQLComandoTransaccion.Parameters.Add("@Status", SqlDbType.Char).Value = ""
                                            SQLComandoTransaccion.Parameters.Add("@Consumo", SqlDbType.Int).Value = 0


                                            SQLTransaccion = ConexionTransaccion.BeginTransaction

                                            SQLComandoTransaccion.Connection = ConexionTransaccion

                                            SQLComandoTransaccion.Transaction = SQLTransaccion


                                            Try
                                                SQLComandoTransaccion.CommandType = CommandType.StoredProcedure
                                                SQLComandoTransaccion.CommandText = "spSTPedidoServiciotecnicoAlta"
                                                SQLComandoTransaccion.CommandTimeout = 300
                                                SQLComandoTransaccion.ExecuteNonQuery()
                                                SQLTransaccion.Commit()
                                            Catch ex As Exception
                                                MessageBox.Show(ex.Message)
                                            Finally
                                                ConexionTransaccion.Close()
                                                'conexiontransaccion.Dispose()
                                                Me.Close()
                                            End Try
                                        Else
                                        End If

                                    Else
                                        If TipoPedido = 10 Then
                                            Dim TipoCredito As Integer
                                            TipoCredito = CType(cboTipoCredito.SelectedValue, Integer)
                                            If TipoCredito = 1 Then
                                                MessageBox.Show("Usted debe de seleccionar un tipo de credito.", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            Else
                                            End If

                                            If MessageBox.Show("¿Son Correctos los Datos?", "servicio Técnicos", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                                'Dim strQuery As String = _
                                                '    "SELECT TotalPuntos FROM vwSTSumaPuntos " & _
                                                '    "where producto = 4 and fcompromiso = '" & dtpFCompromiso.Value.ToShortDateString & "' and celula = " & lblcelula.Text

                                                'Dim daSumaPuntos As New SqlDataAdapter(strQuery, cnnSigamet)
                                                'Dim dtSumaPuntos As New DataTable("Suma")
                                                'daSumaPuntos.Fill(dtSumaPuntos)
                                                'If dtSumaPuntos.Rows.Count > 0 Then
                                                '    Puntos = CType(dtSumaPuntos.Rows(0).Item("totalpuntos"), Integer)
                                                'Else
                                                '    Puntos = CType(0, Integer)
                                                'End If
                                                'Puntos = CType(dtSumaPuntos.Rows(0).Item("totalpuntos"), Integer)
                                                TipoServicio = CType(cboTipoServicio.SelectedValue, Integer)
                                                PuntosTipoServicio()
                                                Dim TotalPuntos As Integer
                                                TotalPuntos = _PuntosTipoServicio + Puntos
                                                If TotalPuntos > _PuntosPermitidos Then
                                                    MessageBox.Show("Usted no puede ingresar un servicio de mas de 3 puntos, Póngase en contacto con Célula 7", "Mensaje de Servicios Técnicos")
                                                    Exit Sub
                                                Else
                                                End If

                                                Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                                                ConexionTransaccion.Open()
                                                Dim SQLComandoTransaccion As New SqlCommand()
                                                Dim SQLTransaccion As SqlTransaction
                                                SQLComandoTransaccion.Parameters.Add("@Observaciones", SqlDbType.VarChar).Value = txtObservaciones.Text
                                                SQLComandoTransaccion.Parameters.Add("@TipoPedido", SqlDbType.Int).Value = cboTipoPedido.SelectedValue
                                                SQLComandoTransaccion.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                                                SQLComandoTransaccion.Parameters.Add("@Cliente", SqlDbType.Int).Value = lblContrato.Text
                                                SQLComandoTransaccion.Parameters.Add("@Celula", SqlDbType.Int).Value = lblcelula.Text
                                                SQLComandoTransaccion.Parameters.Add("@Ruta", SqlDbType.Int).Value = lblRuta.Text
                                                SQLComandoTransaccion.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                                                SQLComandoTransaccion.Parameters.Add("@TipoServicio", SqlDbType.Int).Value = cboTipoServicio.SelectedValue

                                                SQLComandoTransaccion.Parameters.Add("@Total", SqlDbType.Money).Value = lblTotal.Text
                                                SQLComandoTransaccion.Parameters.Add("@Material", SqlDbType.Int).Value = cboMaterial.SelectedValue
                                                SQLComandoTransaccion.Parameters.Add("@Cantidad", SqlDbType.Int).Value = txtCantidad.Text
                                                SQLComandoTransaccion.Parameters.Add("@NumExterior", SqlDbType.Char).Value = NumExterior
                                                SQLComandoTransaccion.Parameters.Add("@NumInterior", SqlDbType.Char).Value = NumInterior
                                                SQLComandoTransaccion.Parameters.Add("@Calle", SqlDbType.Int).Value = Calle
                                                SQLComandoTransaccion.Parameters.Add("@Colonia", SqlDbType.Int).Value = Colonia
                                                SQLComandoTransaccion.Parameters.Add("@DireccionInstalacion", SqlDbType.Char).Value = _Folio
                                                SQLComandoTransaccion.Parameters.Add("@TipoCredito", SqlDbType.Int).Value = cboTipoCredito.SelectedValue

                                                If lblHoraatencion.Text = "" Then
                                                    SQLComandoTransaccion.Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                                                Else
                                                    SQLComandoTransaccion.Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = CType(lblHoraatencion.Text, DateTime)
                                                End If

                                                SQLComandoTransaccion.Parameters.Add("@Parcialidad", SqlDbType.Money).Value = lblPagosde.Text
                                                SQLComandoTransaccion.Parameters.Add("@ImporteLetra", SqlDbType.VarChar).Value = ImporteLetra
                                                SQLComandoTransaccion.Parameters.Add("@NumPagos", SqlDbType.Int).Value = lblNumPagos.Text
                                                SQLComandoTransaccion.Parameters.Add("@Dias", SqlDbType.Int).Value = lblDias.Text


                                                SQLComandoTransaccion.Parameters.Add("@Equipo", SqlDbType.SmallInt).Value = 0
                                                SQLComandoTransaccion.Parameters.Add("@TipoPropiedad", SqlDbType.TinyInt).Value = 0
                                                SQLComandoTransaccion.Parameters.Add("@Serie", SqlDbType.Char).Value = ""
                                                SQLComandoTransaccion.Parameters.Add("@FFabricacion", SqlDbType.DateTime).Value = Nothing
                                                SQLComandoTransaccion.Parameters.Add("@Precio", SqlDbType.Money).Value = 0
                                                SQLComandoTransaccion.Parameters.Add("@FInicioComodato", SqlDbType.DateTime).Value = Nothing
                                                SQLComandoTransaccion.Parameters.Add("@FFinComodato", SqlDbType.DateTime).Value = Nothing
                                                SQLComandoTransaccion.Parameters.Add("@Status", SqlDbType.Char).Value = ""
                                                SQLComandoTransaccion.Parameters.Add("@Consumo", SqlDbType.Int).Value = 0


                                                SQLTransaccion = ConexionTransaccion.BeginTransaction

                                                SQLComandoTransaccion.Connection = ConexionTransaccion

                                                SQLComandoTransaccion.Transaction = SQLTransaccion


                                                Try
                                                    SQLComandoTransaccion.CommandType = CommandType.StoredProcedure
                                                    SQLComandoTransaccion.CommandText = "spSTPedidoServiciotecnicoAlta"
                                                    SQLComandoTransaccion.CommandTimeout = 300
                                                    SQLComandoTransaccion.ExecuteNonQuery()
                                                    SQLTransaccion.Commit()
                                                Catch ex As Exception
                                                    MessageBox.Show(ex.Message)
                                                Finally
                                                    ConexionTransaccion.Close()
                                                    'conexiontransaccion.Dispose()
                                                    Me.Close()
                                                End Try
                                            Else
                                            End If
                                        Else
                                        End If
                                    End If
                                End If
                            End If

                        End If
                    Else
                        If MessageBox.Show("¿Los Datos Son correctos?", "Servicios Técnicos", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            'lblFcompromiso.Text = CType(dtpFCompromiso.Value, String)
                            'Dim strQuery As String = _
                            '    "SELECT TotalPuntos FROM vwSTSumaPuntos " & _
                            '    "where producto = 4 and fcompromiso = '" & dtpFCompromiso.Value.ToShortDateString & "' and celula = " & lblcelula.Text

                            'Dim daSumaPuntos As New SqlDataAdapter(strQuery, cnnSigamet)
                            'Dim dtSumaPuntos As New DataTable("Suma")
                            'daSumaPuntos.Fill(dtSumaPuntos)
                            'If dtSumaPuntos.Rows.Count > 0 Then
                            '    Puntos = CType(dtSumaPuntos.Rows(0).Item("totalpuntos"), Integer)
                            'Else
                            '    Puntos = CType(0, Integer)
                            'End If
                            'Puntos = CType(dtSumaPuntos.Rows(0).Item("totalpuntos"), Integer)
                            TipoServicio = CType(cboTipoServicio.SelectedValue, Integer)
                            PuntosTipoServicio()
                            Dim TotalPuntos As Integer
                            TotalPuntos = _PuntosTipoServicio + Puntos
                            If TotalPuntos > _PuntosPermitidos Then
                                MessageBox.Show("Usted no puede ingresar un servicio de mas de 3 puntos, Póngase en contacto con Célula 7", "Mensaje de Servicios Técnicos")
                                Exit Sub
                            Else
                            End If
                            'If Puntos >= 10 And Puntos <= 12 Then
                            '    If _PuntosTipoServicio >= 3 Then
                            '        MessageBox.Show("Usted no puede ingresar un servicio de mas de 3 puntos, Póngase en contacto con Célula 7", "Mensaje de Servicios Técnicos")
                            '        Exit Sub
                            '    Else

                            '    End If
                            'Else
                            'End If

                            Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                            ConexionTransaccion.Open()
                            Dim SQLComandoTransaccion As New SqlCommand()
                            Dim SQLTransaccion As SqlTransaction
                            SQLComandoTransaccion.Parameters.Add("@Observaciones", SqlDbType.VarChar).Value = txtObservaciones.Text
                            SQLComandoTransaccion.Parameters.Add("@TipoPedido", SqlDbType.Int).Value = cboTipoPedido.SelectedValue
                            SQLComandoTransaccion.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                            SQLComandoTransaccion.Parameters.Add("@Cliente", SqlDbType.Int).Value = lblContrato.Text
                            SQLComandoTransaccion.Parameters.Add("@Celula", SqlDbType.Int).Value = lblcelula.Text
                            SQLComandoTransaccion.Parameters.Add("@Ruta", SqlDbType.Int).Value = lblRuta.Text
                            SQLComandoTransaccion.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                            SQLComandoTransaccion.Parameters.Add("@TipoServicio", SqlDbType.Int).Value = cboTipoServicio.SelectedValue
                            If lblHoraatencion.Text = "" Then
                                SQLComandoTransaccion.Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = dtpFCompromiso.Value
                            Else
                                SQLComandoTransaccion.Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = CType(lblHoraatencion.Text, DateTime)
                            End If

                            SQLComandoTransaccion.Parameters.Add("@Equipo", SqlDbType.SmallInt).Value = 0
                            SQLComandoTransaccion.Parameters.Add("@TipoPropiedad", SqlDbType.TinyInt).Value = 0
                            SQLComandoTransaccion.Parameters.Add("@Serie", SqlDbType.Char).Value = ""
                            SQLComandoTransaccion.Parameters.Add("@FFabricacion", SqlDbType.DateTime).Value = Nothing
                            SQLComandoTransaccion.Parameters.Add("@Precio", SqlDbType.Money).Value = 0
                            SQLComandoTransaccion.Parameters.Add("@FInicioComodato", SqlDbType.DateTime).Value = Nothing
                            SQLComandoTransaccion.Parameters.Add("@FFinComodato", SqlDbType.DateTime).Value = Nothing
                            SQLComandoTransaccion.Parameters.Add("@Status", SqlDbType.Char).Value = ""
                            SQLComandoTransaccion.Parameters.Add("@Consumo", SqlDbType.Int).Value = 0


                            SQLTransaccion = ConexionTransaccion.BeginTransaction

                            SQLComandoTransaccion.Connection = ConexionTransaccion

                            SQLComandoTransaccion.Transaction = SQLTransaccion


                            Try
                                SQLComandoTransaccion.CommandType = CommandType.StoredProcedure
                                SQLComandoTransaccion.CommandText = "spSTPedidoServiciotecnicoAlta"
                                SQLComandoTransaccion.CommandTimeout = 300
                                SQLComandoTransaccion.ExecuteNonQuery()
                                SQLTransaccion.Commit()
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            Finally
                                ConexionTransaccion.Close()
                                'conexiontransaccion.Dispose()
                                Me.Close()
                            End Try
                        Else
                        End If

                    End If
                End If

            Case "Modificar"

                If _StatusServicioTecnico = "ACTIVO" Then

                    Dim SqlConnection As SqlConnection = SigaMetClasses.DataLayer.Conexion
                    Try
                        SqlConnection.Close()
                        'SqlConnection.ConnectionString = ConString
                        SqlConnection.Open()
                    Catch dataException As Exception
                        MsgBox(dataException.Message, MsgBoxStyle.OkOnly, "Mensaje de sistema")
                    End Try



                    Dim Transaccion As SqlClient.SqlTransaction
                    Dim cmdInsert As New SqlClient.SqlCommand()
                    Dim rdrInsert As SqlClient.SqlDataReader = Nothing
                    Dim SiGrabo As Boolean

                    cmdInsert.Connection = SqlConnection
                    cmdInsert.CommandTimeout = 300
                    SiGrabo = False
                    Transaccion = SqlConnection.BeginTransaction
                    cmdInsert.Transaction = Transaccion
                    Try


                        With cmdInsert
                            .CommandType = CommandType.StoredProcedure
                            .CommandText = "spSTModificaPedidoServiciosTecnicos"
                            .Parameters.Clear()
                            .Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value.Date
                            .Parameters.Add("@Observaciones", SqlDbType.Text).Value = txtObservaciones.Text
                            .Parameters.Add("@Tiposervicio", SqlDbType.Int).Value = cboTipoServicio.SelectedValue
                            .Parameters.Add("@Celula", SqlDbType.Int).Value = _celula
                            .Parameters.Add("@Usuario", SqlDbType.VarChar).Value = _Usuario
                            .Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                            .Parameters.Add("@AñoPed", SqlDbType.SmallInt).Value = _AñoPed
                            .Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = CType(Me.lblHoraatencion.Text, Date)

                            .ExecuteNonQuery()

                        End With

                        Transaccion.Commit()

                        SiGrabo = True

                    Catch er As Exception
                        Transaccion.Rollback()
                        MsgBox(er.Message, MsgBoxStyle.Critical)
                        SiGrabo = False
                    Finally
                        If SiGrabo Then
                            Me.Close()
                            If SqlConnection.State = ConnectionState.Open Then
                                SqlConnection.Close()
                            End If
                        End If
                    End Try

                Else
                    MessageBox.Show("Usted no puede modificar un servicio técnico, que ya fue atendido.", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                'Dim SqlConnection As New SqlConnection(ConString)
                'Try
                '    SqlConnection.Close()
                '    SqlConnection.ConnectionString = ConString
                '    SqlConnection.Open()
                'Catch dataException As Exception
                '    MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
                'End Try



                'Dim Transaccion As SqlClient.SqlTransaction
                'Dim cmdInsert As New SqlClient.SqlCommand()
                'Dim rdrInsert As SqlClient.SqlDataReader
                'Dim SiGrabo As Boolean

                'cmdInsert.Connection = SqlConnection
                'cmdInsert.CommandTimeout = 300
                'SiGrabo = False
                'Transaccion = SqlConnection.BeginTransaction
                'cmdInsert.Transaction = Transaccion
                'Try


                '    With cmdInsert
                '        .CommandType = CommandType.StoredProcedure
                '        .CommandText = "spSTModificaPedidoServiciosTecnicos"
                '        .Parameters.Clear()
                '        .Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value.Date
                '        .Parameters.Add("@Observaciones", SqlDbType.Text).Value = txtObservaciones.Text
                '        .Parameters.Add("@Tiposervicio", SqlDbType.Int).Value = cboTipoServicio.SelectedValue
                '        .Parameters.Add("@Celula", SqlDbType.Int).Value = _celula
                '        .Parameters.Add("@Usuario", SqlDbType.VarChar).Value = _Usuario
                '        .Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                '        .Parameters.Add("@AñoPed", SqlDbType.SmallInt).Value = _AñoPed
                '        .Parameters.Add("@FCompromisoInicial", SqlDbType.DateTime).Value = CType(Me.lblHoraatencion.Text, Date)

                '        .ExecuteNonQuery()

                '    End With

                '    Transaccion.Commit()

                '    SiGrabo = True

                'Catch er As Exception
                '    Transaccion.Rollback()
                '    MsgBox(er.Message, MsgBoxStyle.Critical)
                '    SiGrabo = False
                'Finally
                '    If SiGrabo Then
                '        Me.Close()
                '    End If
                'End Try


            Case "Presupuesto"

                If TipoPedido = 7 Or TipoPedido = 8 Or TipoPedido = 10 Then


                    Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                    ConexionTransaccion.Open()
                    Dim SqlComando As New SqlCommand()
                    Dim SqlTransaccion As SqlTransaction
                    SqlComando.Parameters.Add("@Cliente", SqlDbType.Int).Value = lblContrato.Text
                    SqlComando.Parameters.Add("@TipoPedido", SqlDbType.Int).Value = cboTipoPedido.SelectedValue
                    SqlComando.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value.Date
                    SqlComando.Parameters.Add("@Observaciones", SqlDbType.Char).Value = txtObservaciones.Text
                    SqlComando.Parameters.Add("@Celula", SqlDbType.Char).Value = lblcelula.Text
                    SqlComando.Parameters.Add("@Ruta", SqlDbType.Int).Value = lblRuta.Text
                    'SqlComando.Parameters.Add("@StatusServicioTecnico", SqlDbType.Char).Value = lblStatus1.Text
                    SqlComando.Parameters.Add("@TipoServicio", SqlDbType.Int).Value = cboTipoServicio.SelectedValue
                    SqlComando.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                    If _Folio = Nothing Then
                        SqlComando.Parameters.Add("@DireccionInstalacion", SqlDbType.Char).Value = ""
                    Else
                        SqlComando.Parameters.Add("@DireccionInstalacion", SqlDbType.Char).Value = _Folio
                    End If
                    SqlComando.Parameters.Add("@Calle", SqlDbType.Int).Value = Calle
                    SqlComando.Parameters.Add("@Colonia", SqlDbType.Int).Value = Colonia
                    SqlComando.Parameters.Add("@NumExterior", SqlDbType.Char).Value = NumExterior
                    SqlComando.Parameters.Add("@NumInterior", SqlDbType.Char).Value = NumInterior

                    SqlComando.Parameters.Add("@Total", SqlDbType.Money).Value = lblTotal.Text
                    SqlComando.Parameters.Add("@AñofolioPresupuesto", SqlDbType.Char).Value = _AñoFolioPresupuesto
                    SqlComando.Parameters.Add("@FolioPresupuesto", SqlDbType.Int).Value = lblNumPresupuesto.Text
                    SqlComando.Parameters.Add("@Material", SqlDbType.Int).Value = 0
                    SqlComando.Parameters.Add("@TipoCredito", SqlDbType.Int).Value = cboTipoCredito.SelectedValue
                    If lblNumPagos.Text = "" Then
                        SqlComando.Parameters.Add("@NumPagos", SqlDbType.Int).Value = 0
                    Else
                        SqlComando.Parameters.Add("@NumPagos", SqlDbType.Int).Value = lblNumPagos.Text
                    End If

                    If ImporteLetra Is Nothing Then
                        SqlComando.Parameters.Add("@ImporteLetra", SqlDbType.VarChar).Value = ""
                    Else
                        SqlComando.Parameters.Add("@ImporteLetra", SqlDbType.VarChar).Value = ImporteLetra
                    End If
                    If lblDias.Text = "" Then
                        SqlComando.Parameters.Add("@dIAS", SqlDbType.Int).Value = 0
                    Else
                        SqlComando.Parameters.Add("@dIAS", SqlDbType.Int).Value = lblDias.Text
                    End If

                    If lblPagosde.Text = "" Then
                        SqlComando.Parameters.Add("@Parcialidad", SqlDbType.Money).Value = 0
                    Else
                        SqlComando.Parameters.Add("@Parcialidad", SqlDbType.Money).Value = CType(lblPagosde.Text, Integer)
                    End If





                    SqlTransaccion = ConexionTransaccion.BeginTransaction
                    SqlComando.Connection = ConexionTransaccion
                    SqlComando.Transaction = SqlTransaccion
                    Try
                        If MsgBox("¿Quiere Aceptar el Presupuesto?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                            SqlComando.CommandType = CommandType.StoredProcedure
                            SqlComando.CommandText = "spSTGeneraOrdenServicoTecnicoAutomatico"
                            SqlComando.CommandTimeout = 300
                            SqlComando.ExecuteNonQuery()
                            SqlTransaccion.Commit()
                        Else
                            btnPresupuesto.Enabled = False
                            btnAceptar.Enabled = True
                            Exit Try
                        End If


                    Catch eException As Exception
                        SqlTransaccion.Rollback()
                        MsgBox(eException.Message)
                    Finally
                        ConexionTransaccion.Close()
                        'ConexionTransaccion.Dispose()
                        Me.Close()
                    End Try


                Else

                    Dim oEquipo As STClienteEquipo.sClienteEquipoItem
                    Dim oClienteEquipo As New STClienteEquipo.cClienteEquipo()
                    If ClienteEquipo.Equipos.Count > 0 Then

                        If Convert.ToInt32(cboTipoServicio.SelectedValue) = 4 Then
                            MessageBox.Show("Debe seleccionar un servicio diferente a un presupuesto.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            If txtObservaciones.Text = "" Then
                                MessageBox.Show("Debe de capturar las observaciones del cliente.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                If _Folio = "" Then
                                    MessageBox.Show("Debe confirmar la dirección del cliente.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Else
                                    'Componente STClienteEquipo
                                    'SE AGREGA PARA COMPLETAR EL COMODATO

                                    For Each oEquipo In Me.ClienteEquipo.Equipos
                                        oClienteEquipo.Alta(CType(Me.lblContrato.Text, Integer), oEquipo.Equipo, 2, oEquipo.Precio, oEquipo.FFabricacion, oEquipo.Serie, oEquipo.FInicioComodato, oEquipo.FFinComodato, oEquipo.Status, oEquipo.Consumo, _Usuario, _Pedido, _celula, _AñoPed)
                                    Next

                                    Dim ConexionTransaccion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                                    ConexionTransaccion.Open()
                                    Dim SqlComando As New SqlCommand()
                                    Dim SqlTransaccion As SqlTransaction
                                    SqlComando.Parameters.Add("@Cliente", SqlDbType.Int).Value = lblContrato.Text
                                    SqlComando.Parameters.Add("@TipoPedido", SqlDbType.Int).Value = cboTipoPedido.SelectedValue
                                    SqlComando.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value.Date
                                    SqlComando.Parameters.Add("@Observaciones", SqlDbType.Char).Value = txtObservaciones.Text
                                    SqlComando.Parameters.Add("@Celula", SqlDbType.Char).Value = lblcelula.Text
                                    SqlComando.Parameters.Add("@Ruta", SqlDbType.Int).Value = lblRuta.Text
                                    'SqlComando.Parameters.Add("@StatusServicioTecnico", SqlDbType.Char).Value = lblStatus1.Text
                                    SqlComando.Parameters.Add("@TipoServicio", SqlDbType.Int).Value = cboTipoServicio.SelectedValue
                                    SqlComando.Parameters.Add("@Usuario", SqlDbType.Char).Value = _Usuario
                                    If _Folio = Nothing Then
                                        SqlComando.Parameters.Add("@DireccionInstalacion", SqlDbType.Char).Value = ""
                                    Else
                                        SqlComando.Parameters.Add("@DireccionInstalacion", SqlDbType.Char).Value = _Folio
                                    End If
                                    SqlComando.Parameters.Add("@Calle", SqlDbType.Int).Value = Calle
                                    SqlComando.Parameters.Add("@Colonia", SqlDbType.Int).Value = Colonia
                                    SqlComando.Parameters.Add("@NumExterior", SqlDbType.Char).Value = NumExterior
                                    SqlComando.Parameters.Add("@NumInterior", SqlDbType.Char).Value = NumInterior

                                    SqlComando.Parameters.Add("@Total", SqlDbType.Money).Value = lblTotal.Text
                                    SqlComando.Parameters.Add("@AñofolioPresupuesto", SqlDbType.Char).Value = _AñoFolioPresupuesto
                                    SqlComando.Parameters.Add("@FolioPresupuesto", SqlDbType.Int).Value = lblNumPresupuesto.Text
                                    SqlComando.Parameters.Add("@Material", SqlDbType.Int).Value = cboMaterial.SelectedValue
                                    SqlComando.Parameters.Add("@TipoCredito", SqlDbType.Int).Value = cboTipoCredito.SelectedValue
                                    If lblNumPagos.Text = "" Then
                                        SqlComando.Parameters.Add("@NumPagos", SqlDbType.Int).Value = 0
                                    Else
                                        SqlComando.Parameters.Add("@NumPagos", SqlDbType.Int).Value = lblNumPagos.Text
                                    End If

                                    If ImporteLetra Is Nothing Then
                                        SqlComando.Parameters.Add("@ImporteLetra", SqlDbType.VarChar).Value = ""
                                    Else
                                        SqlComando.Parameters.Add("@ImporteLetra", SqlDbType.VarChar).Value = ImporteLetra
                                    End If
                                    If lblDias.Text = "" Then
                                        SqlComando.Parameters.Add("@dIAS", SqlDbType.Int).Value = 0
                                    Else
                                        SqlComando.Parameters.Add("@dIAS", SqlDbType.Int).Value = lblDias.Text
                                    End If

                                    If lblPagosde.Text = "" Then
                                        SqlComando.Parameters.Add("@Parcialidad", SqlDbType.Money).Value = 0
                                    Else
                                        SqlComando.Parameters.Add("@Parcialidad", SqlDbType.Money).Value = CType(lblPagosde.Text, Integer)
                                    End If


                                    SqlTransaccion = ConexionTransaccion.BeginTransaction
                                    SqlComando.Connection = ConexionTransaccion
                                    SqlComando.Transaction = SqlTransaccion
                                    Try
                                        If MsgBox("¿Quiere Aceptar el Presupuesto?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                                            SqlComando.CommandType = CommandType.StoredProcedure
                                            SqlComando.CommandText = "spSTGeneraOrdenServicoTecnicoAutomatico"
                                            SqlComando.CommandTimeout = 300
                                            SqlComando.ExecuteNonQuery()
                                            SqlTransaccion.Commit()

                                        Else
                                            btnPresupuesto.Enabled = False
                                            btnAceptar.Enabled = True
                                            Exit Try
                                        End If


                                    Catch eException As Exception
                                        SqlTransaccion.Rollback()
                                        MsgBox(eException.Message)
                                    Finally
                                        ConexionTransaccion.Close()
                                        'ConexionTransaccion.Dispose()
                                        Me.Close()
                                    End Try
                                End If
                            End If
                        End If

                    Else
                        MessageBox.Show("Usted no le agrego un material al cliente en comodato, agregelo por favor.", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                End If


            Case "Horario"
                Cursor = Cursors.WaitCursor
                Dim hora As New frmhorario(dtpFCompromiso.Value, CType(lblcelula.Text, Byte))
                If hora.ShowDialog() = DialogResult.OK Then
                    Me.lblHoraatencion.Text = hora.HoraSeleccionada.ToString
                End If
                Cursor = Cursors.Default

            Case "Cerrar"
                If MessageBox.Show("¿Desea salir de Servicio Técnicos?", "Servicios Técnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Else
                    Me.Close()
                End If
        End Select

    End Sub

    Private Sub lvwHistorico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwHistorico.SelectedIndexChanged
        _StatusServicioTecnico = lvwHistorico.FocusedItem.SubItems(3).Text.Trim
        _PedidoReferencia = lvwHistorico.FocusedItem.SubItems(0).Text.Trim
        _Presupuesto = lvwHistorico.FocusedItem.SubItems(4).Text.Trim
        LlenaObservaciones()
        LlenaPresupuesto()
        LlenaTecnicos()
        'LlenaTipoPropiedad()

        'carga la etiqueta para la comparacion y de este modo desactivar los botones de servicio 

        lstMaterial.Enabled = False
        If lblStatusPresupuesto.Text.Trim = "ACEPTADO" Then
            btnPresupuesto.Enabled = False
            btnAceptar.Enabled = True
            btnConfirmaDireccion.Enabled = False
            cboTipoCredito.Enabled = False
        Else

            If _Presupuesto = "PRESUPUESTO" Then
                btnAceptar.Enabled = False
                btnPresupuesto.Enabled = True
                btnConfirmaDireccion.Enabled = True
                'CboTipoCredito.Enabled = False
                'lblNumPagos.Enabled = True
                'lblPagosde.Enabled = True
                'lblTotal.Enabled = True
                cboMaterial.Enabled = False
                btnAgregar.Enabled = False
                btnEliminar.Enabled = False
                lstMaterial.Enabled = False
            Else
                btnPresupuesto.Enabled = False
                btnAceptar.Enabled = False
            End If

        End If
        If RTrim(_StatusServicioTecnico) = "ATENDIDO" Then
            btnModificar.Enabled = False
            btnAceptar.Enabled = False
        Else
            btnModificar.Enabled = True
        End If

    End Sub

    Private Sub cboTipoServicio_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ChBSi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnConfirmaDireccion.Enabled = True
        Confirma = True
    End Sub

    Private Sub cboTipoPedido_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoPedido.SelectedIndexChanged

        If _Presupuesto = "PRESUPUESTO" Then
            If DatosCargados Then
                TipoPedido = CType(Me.cboTipoPedido.SelectedValue, Integer)
                If TipoPedido = 7 Then
                    cboMaterial.Enabled = False
                    btnAgregar.Enabled = False
                    btnEliminar.Enabled = False
                    lstMaterial.Enabled = False
                    cboTipoCredito.Enabled = False
                    lblNumPagos.Enabled = False
                    lblPagosde.Enabled = False
                    lblDias.Enabled = False
                    Label21.Enabled = False
                    txtObservaciones.Text = ""
                    txtTrabajoRealizado.Text = ""
                Else
                    If TipoPedido = 10 Then
                        cboMaterial.Enabled = False
                        btnAgregar.Enabled = False
                        btnEliminar.Enabled = False
                        lstMaterial.Enabled = False
                        Label21.Enabled = False
                        cboTipoCredito.Enabled = True
                        cboTipoCredito.Visible = True
                        txtCantidad.Enabled = False
                        'Label19.Visible = True
                        'Label20.Visible = True
                        'Label27.Visible = True
                        'Label25.Visible = True
                        'Label18.Visible = True
                        'Label19.Visible = True
                        'lblNumPagos.Visible = True
                        lblPagosde.Visible = True
                        lblDias.Visible = True
                        lblNumPagos.Enabled = True
                        lblPagosde.Enabled = True
                        lblDias.Enabled = True
                        txtObservaciones.Text = ""
                        txtTrabajoRealizado.Text = ""
                    Else
                        If TipoPedido = 8 Then
                            btnConfirmaDireccion.Enabled = True
                            cboMaterial.Enabled = False
                            btnAgregar.Enabled = False
                            btnEliminar.Enabled = False
                            lstMaterial.Enabled = False
                            Label21.Enabled = False
                            cboTipoCredito.Visible = False
                            lblNumPagos.Enabled = False
                            lblPagosde.Enabled = False
                            lblDias.Enabled = False
                            'Label17.Visible = False
                            'Label27.Enabled = False
                            'Label25.Enabled = False
                            'Label31.Enabled = False
                            'Label18.Enabled = False
                            'Label20.Enabled = False
                            ClienteEquipo.Enabled = False
                            txtObservaciones.Text = ""
                            txtTrabajoRealizado.Text = ""
                        Else
                            If TipoPedido = 9 Then
                                If CargaCombos = True Then
                                    ValidaComodato()
                                Else
                                End If
                                ClienteEquipo.Enabled = True
                                btnConfirmaDireccion.Enabled = True
                                txtObservaciones.Text = ""
                                txtTrabajoRealizado.Text = ""
                            Else
                            End If
                        End If
                    End If
                End If
            Else
            End If
        Else
            If DatosCargados Then
                TipoPedido = CType(Me.cboTipoPedido.SelectedValue, Integer)
                If TipoPedido = 7 Then
                    btnConfirmaDireccion.Enabled = True
                    cboMaterial.Enabled = True
                    btnAgregar.Enabled = True
                    btnEliminar.Enabled = True
                    lstMaterial.Enabled = True
                    Label21.Enabled = True
                    txtCantidad.Enabled = True
                    cboTipoCredito.Enabled = False
                    lblNumPagos.Enabled = False
                    lblPagosde.Enabled = False
                    lblDias.Enabled = False
                    'Label27.Enabled = False
                    'Label25.Enabled = False
                    'Label31.Enabled = False
                    'Label18.Enabled = False
                    'Label19.Enabled = False
                    'Label20.Enabled = False
                    ClienteEquipo.Enabled = False
                    txtObservaciones.Text = ""
                    txtTrabajoRealizado.Text = ""
                Else
                    If TipoPedido = 9 Then
                        If CargaCombos = True Then
                            ValidaComodato()
                        Else
                        End If
                        ClienteEquipo.Enabled = True
                        btnConfirmaDireccion.Enabled = True
                    Else
                        If TipoPedido = 10 Then
                            btnConfirmaDireccion.Enabled = True
                            cboMaterial.Enabled = True
                            btnAgregar.Enabled = True
                            btnEliminar.Enabled = True
                            lstMaterial.Enabled = True
                            Label21.Enabled = True
                            cboTipoCredito.Enabled = True
                            cboTipoCredito.Enabled = True
                            lblNumPagos.Enabled = True
                            lblNumPagos.Enabled = True
                            lblPagosde.Enabled = True
                            lblPagosde.Enabled = True
                            lblDias.Enabled = True
                            lblDias.Enabled = True
                            'Label17.Visible = True
                            'Label27.Enabled = True
                            'Label25.Enabled = True
                            'Label31.Enabled = True
                            'Label18.Enabled = True
                            'Label20.Enabled = True
                            ClienteEquipo.Enabled = False
                            txtObservaciones.Text = ""
                            txtTrabajoRealizado.Text = ""
                            txtCantidad.Enabled = True
                        Else
                            If TipoPedido = 8 Then
                                btnConfirmaDireccion.Enabled = False
                                cboMaterial.Enabled = False
                                btnAgregar.Enabled = False
                                btnEliminar.Enabled = False
                                lstMaterial.Enabled = False
                                Label21.Enabled = False
                                cboTipoCredito.Enabled = False
                                lblNumPagos.Enabled = False
                                lblPagosde.Enabled = False
                                lblDias.Enabled = False
                                'Label17.Visible = False
                                'Label27.Enabled = False
                                'Label25.Enabled = False
                                'Label31.Enabled = False
                                'Label18.Enabled = False
                                'Label20.Enabled = False
                                ClienteEquipo.Enabled = False
                                txtObservaciones.Text = ""
                                txtTrabajoRealizado.Text = ""
                            Else
                            End If
                        End If
                    End If
                End If

            Else

            End If
        End If
    End Sub

    Private Sub btnConfirmaDireccion_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirmaDireccion.Click
        Cursor = Cursors.WaitCursor
        Dim ConfirmaDireccion As New frmConfirmaDireccion(CType(lblContrato.Text, Integer))
        ConfirmaDireccion.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub btnAgregar_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Dim Material As Integer
        Material = CType(cboMaterial.SelectedValue, Integer)
        Dim ConsMaterial As String = "select material,Descripcion + ' $ ' +cast(Precio as varchar (10))as descripcion,isnull(precio,0) AS Precio,UnidadMedida,Precio  from Material where material = " & Material
        Dim sqlMaterial As New SqlDataAdapter(ConsMaterial, cnnSigamet)
        Dim dtMaterial As New DataTable()
        Dim Cantidad As Decimal
        sqlMaterial.Fill(dtMaterial)
        'lstMaterial.Items.Add(dtMaterial.Rows(0).Item("Descripcion"))

        Cuenta = CType(dtMaterial.Rows(0).Item("precio"), Integer)
        _Material = CType(dtMaterial.Rows(0).Item("material"), Integer)
        If Cuenta <> 0 Then
            lstMaterial.Items.Add(dtMaterial.Rows(0).Item("Descripcion"))
            _SubTotal = Cuenta
            'InsertaMaterial()
            If Suma > 0 Then
                Cantidad = CType(txtCantidad.Text, Decimal)
                Suma = CType(lblTotal.Text, Integer)
                Suma = CType(Cantidad, Integer) * Cuenta + Suma
                lblTotal.Text = CType(Suma, String)
            Else
                Cantidad = CType(txtCantidad.Text, Decimal)
                Suma = 0
                Suma = CType(Cantidad, Integer) * Cuenta + Suma
                lblTotal.Text = CType(Format(Suma, "###,###.##"), String)
            End If
        Else
            MessageBox.Show("El material no tiene precio, Pongase en contacto con célula 7", "Servicios Técnicos", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub btnEliminar_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        lstMaterial.Items.Remove(lstMaterial.SelectedItem)
        'txtPrecio.Text = " "
        'txtCantidad.Text = ""
        lblNumPagos.Text = ""
        lblDias.Text = ""
        lblPagosde.Text = ""
        lblTotal.Text = CType(0, String)
        txtCantidad.Text = CType(1, String)
    End Sub

    Private Sub cboTipoCredito_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoCredito.SelectedIndexChanged
        Dim TipoCredito As String
        TipoCredito = CType(cboTipoCredito.Text, String)
        If TipoCredito = "SIN CREDITO" Then
            lblNumPagos.Text = ""
            lblPagosde.Text = ""
            lblDias.Text = ""
        Else
            Dim Consulta As String
            Consulta = "select creditoserviciotecnico,descripcion, NumeroPagos,frecuencia from creditoserviciotecnico where descripcion = '" & TipoCredito & "'"
            Dim da As New SqlDataAdapter(Consulta, cnnSigamet)
            Dim dt As New DataTable("Pagare")
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                lblNumPagos.Text = CType(dt.Rows(0).Item("numeropagos"), String)
                lblDias.Text = CType(dt.Rows(0).Item("frecuencia"), String)
            End If


            If lblTotal.Text > "" Then
                'Dim a As New CantALetra.cCantALetraClass()

                Dim Importe As String = Nothing

                lblPagosde.Text = CType(Format(CType(lblTotal.Text, Double) / CType(lblNumPagos.Text, Double), "###,###.##"), String)
                'lblPagosde.Text = Format(lblPagosde.Text, "###,###.##")
                If lblPagosde.Text = "" Then
                    lblPagosde.Text = CType(0, String)
                Else
                End If
                'a.Numero = CType((lblPagosde.Text), Double)
                'a.Unidad = "M.N."
                'a.Moneda = "Pesos"
                Dim a As New CantidadEnLetra.CantidadEnLetra(CDec(lblPagosde.Text))
                ImporteLetra = "**(" & a.CantidadEnLetras & ")**"
            End If
        End If
    End Sub

    Private Sub dtpFCompromiso_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFCompromiso.ValueChanged
        If cnnSigamet.State = ConnectionState.Open Then
            cnnSigamet.Close()
            Exit Sub
        Else
        End If

        Dim DiaFestivo As Integer
        Dim MesFestivo As Integer
        Dim Añofestivo As Integer
        Dim DiaFcompromiso As Integer
        'Dim Quediaes As String
        Dim MesFCompromiso As Integer
        Dim consulta As New SqlCommand("select Dia,Mes,Año from Festivo where año = 0", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drFestivo As SqlDataReader = consulta.ExecuteReader
            While drFestivo.Read
                DiaFestivo = CType(drFestivo("Dia"), Integer)
                MesFestivo = CType(drFestivo("Mes"), Integer)
                Añofestivo = CType(drFestivo("año"), Integer)
                DiaFcompromiso = dtpFCompromiso.Value.Day
                'Quediaes = CType(dtpFCompromiso.Value.DayOfWeek, String)
                MesFCompromiso = dtpFCompromiso.Value.Month
                If DiaFestivo = DiaFcompromiso And MesFestivo = MesFCompromiso Then
                    MessageBox.Show("Usted no puede ingresar servicios técnicos en esta fecha, pues es día festivo, seleccione otra fecha por favor.", "Mensaje de Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    dtpFCompromiso.Value = dtpFCompromiso.Value.AddDays(1)
                    Exit Try
                Else
                End If
            End While
            cnnSigamet.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Private Sub txtObservaciones_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtObservaciones.Validated
        If Len(txtObservaciones.Text) > 150 Then
            MessageBox.Show("Su texto es demasiado grande, debe de recortarlo para poder guardar su servicio técnico", "Servicios Técnicos", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
        End If
    End Sub

    Private Sub lblContrato_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblContrato.Click

    End Sub
End Class
