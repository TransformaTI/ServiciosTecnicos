Imports System.Data.SqlClient
Public Class frmSelecLiquidacion
    Inherits System.Windows.Forms.Form

    Public Pedido As Integer
    Public AñoPed As Integer
    Public Celula As Integer
    Public PedidoReferencia As String
    Public Total As Integer
    Public Creditos As Decimal
    Public contados As Decimal
    Public TotalCliente As Integer
    Public _Usuario As String
    Public Cobro As Integer
    Public AñoCobro As Integer
    Public Folio As Integer
    Public AñoAtt As Integer
    Public TipoCobro As Integer
    Public NumCheque As Integer
    Public Parcialidad As Integer
    Public Banco As Integer
    Public StatusServicioTecnico As String
    Public Saldo As Integer
    Public Status As String
    Public StatusLogistica As String
    Public Diferencia As Integer
    Private Sub llenacombo()
        Dim Liquidacion As String
        Liquidacion = "select autotanque from autotanque where tipoproducto = 2"
        Dim da As New SqlDataAdapter(Liquidacion, cnnSigamet)
        Dim ds As New DataSet()
        da.Fill(ds, "Liquidacion")
        With cboLiquidacion
            .DataSource = ds.Tables("liquidacion")
            .DisplayMember = "autotanque"
            .ValueMember = "autotanque"
        End With

        Liquidacion = " "
    End Sub

    Private Sub LlenaGrid()
        Me.grdLiquidacion.DataSource = Nothing
        Dim da As New SqlDataAdapter("select Cliente,PedidoReferencia,TipoPedido,TipoServicio,FCompromiso,Total,Status,StatusServicioTecnico,TipoCobroDescripcion,Folio,AñoAtt from vwstliquidacionserviciotecnico " _
                                      & "where fcompromiso >= ' " & dtpLiquidacion.Value.ToShortDateString & " 00:00:00' " _
                                      & "and fcompromiso <= ' " & dtpLiquidacion.Value.ToShortDateString & " 23:59:59' " _
                                      & "and autotanque = ' " & cboLiquidacion.Text & " ' ", cnnSigamet)
        Dim dt As New DataTable("liquidacion")
        da.fill(dt)
        Me.grdLiquidacion.DataSource = dt

    End Sub

    Private Sub SumaCreditos()
        Dim daCreditos As New SqlDataAdapter("select isnull(sum(Total),0)as Total from vwstliquidacionserviciotecnico " _
                                      & "where tipocobro = 8 and StatusServicioTecnico = 'ATENDIDO' and fcompromiso >= ' " & dtpLiquidacion.Value.ToShortDateString & " 00:00:00' " _
                                      & "and fcompromiso <= ' " & dtpLiquidacion.Value.ToShortDateString & " 23:59:59' " _
                                      & "and autotanque = ' " & cboLiquidacion.Text & " ' ", cnnSigamet)
        Dim dtCreditos As New DataTable("liquidacion")
        daCreditos.Fill(dtCreditos)
        Creditos = CType(dtCreditos.Rows(0).Item("Total"), Decimal)
        lblCreditos.Text = CType(Format(Creditos, "###,###.##"), String)
    End Sub

    Private Sub SumaContados()
        Dim daCreditos As New SqlDataAdapter("select isnull(sum(Total),0)as Total from vwstliquidacionserviciotecnico " _
                                              & "where tipocobro = 5 and StatusServicioTecnico = 'ATENDIDO' and fcompromiso >= ' " & dtpLiquidacion.Value.ToShortDateString & " 00:00:00' " _
                                              & "and fcompromiso <= ' " & dtpLiquidacion.Value.ToShortDateString & " 23:59:59' " _
                                              & "and autotanque = ' " & cboLiquidacion.Text & " ' ", cnnSigamet)
        Dim dtCreditos As New DataTable("liquidacion")
        daCreditos.Fill(dtCreditos)
        contados = CType(dtCreditos.Rows(0).Item("Total"), Decimal)
        'lblContados.Text = CType(Format(contados, "###,###.##"), String)
        lblContados.Text = CType(contados, String)
    End Sub
    Private Sub SumaTotal()
        lblCreditos.Text = Format(Creditos, "###,###.##")
        lblContados.Text = Format(contados, "###,###.##")
        lblTotal.Text = Format(Creditos + contados, "###,###.##")
    End Sub

    Private Sub SumaParcialidad()
        Dim da As New SqlDataAdapter("select importecontado from autotanqueturno where folio = " & Folio & "and añoatt = " & AñoAtt, cnnSigamet)
        Dim dt As New DataTable("SumaParcialidad")
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            Parcialidad = CType(dt.Rows(0).Item("importecontado"), Integer)
            contados = Parcialidad + contados
            Creditos = Creditos - Parcialidad
        Else
        End If

    End Sub

    Private Sub LlenaDetalle()
        Dim daDetalle As New SqlCommand("select Celula,Ruta,Nombre,RazonSocial,CalleNombre,NumExterior,NumInterior,ColoniaNombre,CP,MunicipioNombre " _
                                             & "from vwSTClienteServicioTecnico where cliente = ' " & lblCliente.Text & " ' ", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim dr As SqlDataReader = daDetalle.ExecuteReader

            While dr.Read
                lblCelula.Text = CType(dr("celula"), String)
                lblRuta.Text = CType(dr("ruta"), String)
                lblNombre.Text = CType(dr("nombre"), String)
                lblEmpresa.Text = CType(dr("razonsocial"), String)
                lblCalle.Text = CType(dr("callenombre"), String)
                lblNumeroExterior.Text = CType(dr("numexterior"), String)
                lblNumeroInterior.Text = CType(dr("numInterior"), String)
                lblColonia.Text = CType(dr("ColoniaNombre"), String)
                lblCP.Text = CType(dr("cp"), String)
                lblMunicipio.Text = CType(dr("municipionombre"), String)
            End While

            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)

        End Try


    End Sub



    Private Sub LlenaServicio()
        Dim Datos As New SqlDataAdapter("select pedido,celula,añoped,isnull(Total,0) as Total from pedido where pedidoreferencia = '" & PedidoReferencia & "' ", cnnSigamet)
        Try
            Dim dtDatos As New DataTable("Pedidos")
            Datos.Fill(dtDatos)
            If dtDatos.Rows.Count <> 0 Then
                Pedido = CType(dtDatos.Rows(0).Item("Pedido"), Integer)
                Celula = CType(dtDatos.Rows(0).Item("Celula"), Integer)
                AñoPed = CType(dtDatos.Rows(0).Item("Añoped"), Integer)
                TotalCliente = CType(dtDatos.Rows(0).Item("total"), Integer)
            Else
            End If

        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try


        Dim daServicio As New SqlCommand("select ObservacionesServicioTecnico,Autotanque,Folio,Chofer,Ayudante " _
                                          & "from vwSTPestanaServicioTecnico WHERE pedido = " & Pedido & "and celula = " & Celula & "and añoped = ' " & AñoPed & " ' ", cnnSigamet)
        Try
            cnnSigamet.Open()
            Dim drServicio As SqlDataReader = daServicio.ExecuteReader
            While drServicio.Read
                txtTrabajoRealizado.Text = CType(drServicio("ObservacionesServicioTecnico"), String)
                lblUnidad.Text = CType(drServicio("autotanque"), String)
                lblTecnico.Text = CType(drServicio("Chofer"), String)
                lblAyudante.Text = CType(drServicio("Ayudante"), String)
            End While
            cnnSigamet.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

    End Sub

    Private Sub llenaCheque()
        
        Dim daCheque As New SqlDataAdapter("select NumeroCheque,NumeroCuenta,Pedido,Monto,Banco,Disponible,Status,Cobro,AñoCobro from vwSTConsultaChequeServicioTecnico " _
                                              & "where Status <> 'CANCELADO' and fcompromiso >= '" & dtpLiquidacion.Value.ToShortDateString & " 00:00:00' " _
                                              & "and fcompromiso <= '" & dtpLiquidacion.Value.ToShortDateString & " 23:59:59' " _
                                              & "and autotanque = '" & cboLiquidacion.Text & "' ", cnnSigamet)
        Try

            Dim dtCheque As New DataTable("Cheque")
            daCheque.Fill(dtCheque)
            If dtCheque.Rows.Count <> 0 Then
                Me.grdCheques.DataSource = dtCheque

            Else
                Me.grdCheques.DataSource = Nothing
            End If

        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

    End Sub

    Private Sub VerificaCobro()
       
        Dim daCobro As New SqlDataAdapter("select Cobro,AñoCobro,total,Banco,Saldo from vwSTVerificaCobro " _
                                              & "where status = 'Activo'and fcompromiso >= '" & dtpLiquidacion.Value.ToShortDateString & " 00:00:00' " _
                                              & "and fcompromiso <= '" & dtpLiquidacion.Value.ToShortDateString & " 23:59:59' " _
                                              & "and autotanque = '" & cboLiquidacion.Text & "' ", cnnSigamet)
        Dim dtVerificacobro As New DataTable("VerificaCobro")
        daCobro.Fill(dtVerificacobro)

        If TipoCobro = 8 Then

            If dtVerificacobro.Rows.Count <> 0 Then
                Cobro = 0
                AñoCobro = 0
            Else



            End If
        Else
            If dtVerificacobro.Rows.Count <> 0 Then
                Dim i As Integer
                i = 0
                While i < dtVerificacobro.Rows.Count
                    Cobro = CType(dtVerificacobro.Rows(i).Item("cobro"), Integer)
                    AñoCobro = CType(dtVerificacobro.Rows(i).Item("añocobro"), Integer)
                    Banco = CType(dtVerificacobro.Rows(i).Item("banco"), Integer)
                    Saldo = CType(dtVerificacobro.Rows(i).Item("saldo"), Integer)
                    If Banco = 0 Then
                        If Saldo = 0 Then
                            Exit While
                        Else
                        End If
                    Else
                        If Saldo = 0 Then
                            AñoCobro = 0
                            Cobro = 0

                        Else
                            If Saldo = 1 Then
                                AñoCobro = 0
                                Cobro = 0
                            Else
                            End If
                        End If
                    End If
                    i = i + 1
                End While


            Else
                Cobro = 0
                AñoCobro = 0
            End If
        End If

    End Sub
    Private Sub VerificaCheque()
        Dim daVerificaCheque As New SqlDataAdapter("select NumeroCheque from vwstverificacobro where tipocobro = 3 and pedido = " & Pedido & " and celula = " & Celula & " and añoped = " & AñoPed, cnnSigamet)
        Dim dtVerificaCheque As New DataTable("VerificaCheque")
        daVerificaCheque.Fill(dtVerificaCheque)
        If dtVerificaCheque.Rows.Count <> 0 Then
            NumCheque = CType(dtVerificaCheque.Rows(0).Item("numerocheque"), Integer)
        Else
            NumCheque = 0
        End If
    End Sub

    Private Sub VerificaAtendidos()
        Dim da As New SqlDataAdapter("select Cliente,PedidoReferencia,TipoPedido,TipoServicio,FCompromiso,Total,Status,StatusServicioTecnico,TipoCobroDescripcion,Folio,AñoAtt from vwstliquidacionserviciotecnico " _
                                     & "where fcompromiso >= ' " & dtpLiquidacion.Value.ToShortDateString & " 00:00:00' " _
                                     & "and fcompromiso <= ' " & dtpLiquidacion.Value.ToShortDateString & " 23:59:59' " _
                                     & "and autotanque = ' " & cboLiquidacion.Text & " ' ", cnnSigamet)
        Dim dt As New DataTable("Atendido")
        da.Fill(dt)
        If dt.Rows.Count <> 0 Then
            Dim i As Integer
            While i < dt.Rows.Count
                Status = RTrim(CType(dt.Rows(i).Item("statusserviciotecnico"), String))
                If Status = "ACTIVO" Then
                    Exit While
                Else
                End If
                i = i + 1
            End While
        Else
        End If
    End Sub

    Private Sub StatusServicioTec()
        Dim daStatus As New SqlDataAdapter("select StatusServicioTecnico from ServicioTecnico where  pedido = " & Pedido & " and celula = " & Celula & " and añoped = " & AñoPed, cnnSigamet)
        Dim dtStatus As New DataTable("StatusServicioTecnico")
        daStatus.Fill(dtStatus)
        If dtStatus.Rows.Count <> 0 Then
            StatusServicioTecnico = RTrim(CType(dtStatus.Rows(0).Item("statusserviciotecnico"), String))
        Else
        End If
    End Sub

    'Private Sub NoRepetirCobro()
    '    Dim daNoRepetir As New SqlDataAdapter("select Cobro,AñoCobro from vwSTVerificaCobro where pedidoreferencia = '" & PedidoReferencia & "'", cnnSigamet)
    '    Dim dtNoRepetir As New DataTable("NoRepetirCobro")
    '    daNoRepetir.Fill(dtNoRepetir)
    '    If dtNoRepetir.Rows.Count > 0 Then
    '        MessageBox.Show("Ya capturo este pedido, seleccione otro.", "Servicios Técnicos", MessageBoxButtons.OK)
    '    Else
    '    End If
    'End Sub

    Private Sub DatosCheque()
        Dim daDatosCheque As New SqlDataAdapter("select Cobro,AñoCobro from cobro where cobro = " & Cobro & " and añocobro = " & AñoCobro, cnnSigamet)
        Dim dtDatosCheque As New DataTable("DatosCheque")
        daDatosCheque.Fill(dtDatosCheque)
        If dtDatosCheque.Rows.Count <> 0 Then
            Cobro = CType(dtDatosCheque.Rows(0).Item("cobro"), Integer)
            AñoCobro = CType(dtDatosCheque.Rows(0).Item("AñoCobro"), Integer)
        Else
            Cobro = 0
            AñoCobro = 0
        End If

    End Sub

    Private Sub SumaKilometraje()
        Diferencia = CType(CType(txtKilometrajeFinal.Text, Double) - CType(txtKilometrajeInicial.Text, Double), Integer)
    End Sub

    Private Sub VerificaTipoCobro()
        Dim daTipoCobro As New SqlDataAdapter("select isnull(TipoCobro,0) as TipoCobro From Pedido where Pedido = " & Pedido & "and celula = " & Celula & " and AñoPed = '" & AñoPed & "'", cnnSigamet)
        Dim dtTipoCobro As New DataTable("TipoCobro")
        daTipoCobro.Fill(dtTipoCobro)
        If dtTipoCobro.Rows.Count <> 0 Then
            TipoCobro = CType(dtTipoCobro.Rows(0).Item("tipocobro"), Integer)

        Else
        End If

    End Sub
    Private Sub StatusFolio()
        Dim daStatus As New SqlDataAdapter("select statuslogistica from autotanqueturno where folio = " & Folio & " and añoatt = " & AñoAtt, cnnSigamet)
        Dim dtStatus As New DataTable("Status")
        daStatus.Fill(dtStatus)
        StatusLogistica = RTrim(CType(dtStatus.Rows(0).Item("statuslogistica"), String))
    End Sub

    Private Sub ExisteCheque()
        Try
            Dim daExisteCheque As New SqlDataAdapter("select Cliente,NumeroCheque,Total,Cobro From vwSTConsultaChequeServicioTecnico " _
                                                 & "where status <> 'CANCELADO' and pedido = " & Pedido & " and celula =" & Celula & "and añoped = '" & AñoPed & "'", cnnSigamet)
            Dim dtExisteCheque As New DataTable("ExisteCheque")
            daExisteCheque.Fill(dtExisteCheque)
            'Dim NumCheque As String
            If dtExisteCheque.Rows.Count <> 0 Then
                NumCheque = CType(RTrim(CType(dtExisteCheque.Rows(0).Item("numerocheque"), String)), Integer)
            Else
                NumCheque = 0
            End If
        Catch e As Exception
            MessageBox.Show(e.Message)
        Finally
            cnnSigamet.Close()
            'cnnSigamet.Dispose()
        End Try
        ''Dim daExisteCheque As New SqlDataAdapter("select Cliente,NumeroCheque,Total,Cobro From vwSTConsultaChequeServicioTecnico " _
        ''                                         & "where status <> 'CANCELADO' and pedido = " & Pedido & " and celula =" & Celula & "and añoped = '" & AñoPed & "'", cnnSigamet)
        ''Dim dtExisteCheque As New DataTable("ExisteCheque")
        ''daExisteCheque.Fill(dtExisteCheque)
        '''Dim NumCheque As String
        ''If dtExisteCheque.Rows.Count <> 0 Then
        ''    NumCheque = CType(RTrim(CType(dtExisteCheque.Rows(0).Item("numerocheque"), String)), Integer)
        ''Else
        ''    NumCheque = 0
        ''End If
        'NumCheque = RTrim(CType(dtExisteCheque.Rows(0).Item("numerocheque"), String))
        'If dtExisteCheque.Rows.Count > 0 Then
        '    NumCheque = RTrim(CType(dtExisteCheque.Rows(0).Item("numerocheque"), String))
        '    If dtExisteCheque.Rows.Count > 0 Then
        '        If MessageBox.Show("¿Desea modificar el cheque?", "Servico Técnico", MessageBoxButtons.YesNo) = DialogResult.Yes Then
        '        Else
        '            MessageBox.Show("El pedido " + CType(Pedido, String) + ". Tiene asignado el cheque " + NumCheque + ". No puede asignarle otro cheque", "Servicios Técnicos", MessageBoxButtons.OK)
        '        End If
        '    Else
        '    End If
        'Else
        'End If

    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Usuario As String)
        MyBase.New()
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
    Friend WithEvents tbLiquidacion As System.Windows.Forms.ToolBar
    Friend WithEvents tbnRefrescar As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents cboLiquidacion As System.Windows.Forms.ComboBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents grdLiquidacion As System.Windows.Forms.DataGrid
    Friend WithEvents dtpLiquidacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents tbnCerrarOrden As System.Windows.Forms.ToolBarButton
    Friend WithEvents DGTSLiquidacion As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DGTBCCliente As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBTipoPedido As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBTipoServicio As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBFCompromiso As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBTotal As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBStatus As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBTipoCobro As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblCelula As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents lblMunicipio As System.Windows.Forms.Label
    Friend WithEvents lblCP As System.Windows.Forms.Label
    Friend WithEvents lblColonia As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents lblNumeroInterior As System.Windows.Forms.Label
    Friend WithEvents lblNumeroExterior As System.Windows.Forms.Label
    Friend WithEvents lblCalle As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents lblEmpresa As System.Windows.Forms.Label
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblAyudante As System.Windows.Forms.Label
    Friend WithEvents lblTecnico As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents lblUnidad As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtTrabajoRealizado As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblCreditos As System.Windows.Forms.Label
    Friend WithEvents lblContados As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DGTBPedidoReferencia As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents tbnAceptar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCheque As System.Windows.Forms.ToolBarButton
    Friend WithEvents grdCheques As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DGTBCNumeroCheque As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCNumeroCuenta As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCMonto As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCBanco As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCDisponible As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCPedido As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCCobro As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCAñoCobro As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCFolio As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCAñoAtt As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DGTBCStatusServicioTecnico As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lblSumaParcialidad As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnCancelarCheque As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnActivaOrden As System.Windows.Forms.ToolBarButton
    Friend WithEvents txtKilometrajeInicial As System.Windows.Forms.TextBox
    Friend WithEvents txtKilometrajeFinal As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSelecLiquidacion))
        Me.tbLiquidacion = New System.Windows.Forms.ToolBar()
        Me.tbnAceptar = New System.Windows.Forms.ToolBarButton()
        Me.tbnCerrarOrden = New System.Windows.Forms.ToolBarButton()
        Me.btnActivaOrden = New System.Windows.Forms.ToolBarButton()
        Me.btnCheque = New System.Windows.Forms.ToolBarButton()
        Me.btnCancelarCheque = New System.Windows.Forms.ToolBarButton()
        Me.tbnRefrescar = New System.Windows.Forms.ToolBarButton()
        Me.tbnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.cboLiquidacion = New System.Windows.Forms.ComboBox()
        Me.grdLiquidacion = New System.Windows.Forms.DataGrid()
        Me.DGTSLiquidacion = New System.Windows.Forms.DataGridTableStyle()
        Me.DGTBCCliente = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBPedidoReferencia = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBTipoPedido = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBTipoServicio = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBFCompromiso = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBTotal = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBStatus = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCStatusServicioTecnico = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBTipoCobro = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCFolio = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCAñoAtt = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dtpLiquidacion = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.lblCelula = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.lblMunicipio = New System.Windows.Forms.Label()
        Me.lblCP = New System.Windows.Forms.Label()
        Me.lblColonia = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.lblNumeroInterior = New System.Windows.Forms.Label()
        Me.lblNumeroExterior = New System.Windows.Forms.Label()
        Me.lblCalle = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.lblEmpresa = New System.Windows.Forms.Label()
        Me.lblNombre = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblSumaParcialidad = New System.Windows.Forms.Label()
        Me.lblAyudante = New System.Windows.Forms.Label()
        Me.lblTecnico = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.lblUnidad = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtTrabajoRealizado = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblCreditos = New System.Windows.Forms.Label()
        Me.lblContados = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.grdCheques = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DGTBCNumeroCheque = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCNumeroCuenta = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCPedido = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCMonto = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCBanco = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCDisponible = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCCobro = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DGTBCAñoCobro = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtKilometrajeInicial = New System.Windows.Forms.TextBox()
        Me.txtKilometrajeFinal = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        CType(Me.grdLiquidacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdCheques, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbLiquidacion
        '
        Me.tbLiquidacion.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.tbLiquidacion.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbnAceptar, Me.tbnCerrarOrden, Me.btnActivaOrden, Me.btnCheque, Me.btnCancelarCheque, Me.tbnRefrescar, Me.tbnCerrar})
        Me.tbLiquidacion.DropDownArrows = True
        Me.tbLiquidacion.ImageList = Me.ImageList1
        Me.tbLiquidacion.Name = "tbLiquidacion"
        Me.tbLiquidacion.ShowToolTips = True
        Me.tbLiquidacion.Size = New System.Drawing.Size(866, 39)
        Me.tbLiquidacion.TabIndex = 0
        '
        'tbnAceptar
        '
        Me.tbnAceptar.ImageIndex = 3
        Me.tbnAceptar.Text = "Aceptar"
        '
        'tbnCerrarOrden
        '
        Me.tbnCerrarOrden.ImageIndex = 0
        Me.tbnCerrarOrden.Text = "Cerrar Orden"
        '
        'btnActivaOrden
        '
        Me.btnActivaOrden.ImageIndex = 7
        Me.btnActivaOrden.Text = "Act.Orden"
        '
        'btnCheque
        '
        Me.btnCheque.ImageIndex = 4
        Me.btnCheque.Text = "Cheques"
        '
        'btnCancelarCheque
        '
        Me.btnCancelarCheque.ImageIndex = 6
        Me.btnCancelarCheque.Text = "CancelarCheque"
        '
        'tbnRefrescar
        '
        Me.tbnRefrescar.ImageIndex = 1
        Me.tbnRefrescar.Text = "Refrescar"
        '
        'tbnCerrar
        '
        Me.tbnCerrar.ImageIndex = 2
        Me.tbnCerrar.Text = "Cerrar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'cboLiquidacion
        '
        Me.cboLiquidacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLiquidacion.Location = New System.Drawing.Point(536, 8)
        Me.cboLiquidacion.Name = "cboLiquidacion"
        Me.cboLiquidacion.Size = New System.Drawing.Size(104, 21)
        Me.cboLiquidacion.TabIndex = 1
        '
        'grdLiquidacion
        '
        Me.grdLiquidacion.DataMember = ""
        Me.grdLiquidacion.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdLiquidacion.Location = New System.Drawing.Point(0, 40)
        Me.grdLiquidacion.Name = "grdLiquidacion"
        Me.grdLiquidacion.ReadOnly = True
        Me.grdLiquidacion.Size = New System.Drawing.Size(952, 216)
        Me.grdLiquidacion.TabIndex = 2
        Me.grdLiquidacion.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DGTSLiquidacion})
        '
        'DGTSLiquidacion
        '
        Me.DGTSLiquidacion.DataGrid = Me.grdLiquidacion
        Me.DGTSLiquidacion.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DGTBCCliente, Me.DGTBPedidoReferencia, Me.DGTBTipoPedido, Me.DGTBTipoServicio, Me.DGTBFCompromiso, Me.DGTBTotal, Me.DGTBStatus, Me.DGTBCStatusServicioTecnico, Me.DGTBTipoCobro, Me.DGTBCFolio, Me.DGTBCAñoAtt})
        Me.DGTSLiquidacion.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DGTSLiquidacion.MappingName = "liquidacion"
        '
        'DGTBCCliente
        '
        Me.DGTBCCliente.Format = ""
        Me.DGTBCCliente.FormatInfo = Nothing
        Me.DGTBCCliente.HeaderText = "Cliente"
        Me.DGTBCCliente.MappingName = "Cliente"
        Me.DGTBCCliente.Width = 75
        '
        'DGTBPedidoReferencia
        '
        Me.DGTBPedidoReferencia.Format = ""
        Me.DGTBPedidoReferencia.FormatInfo = Nothing
        Me.DGTBPedidoReferencia.HeaderText = "Pedido"
        Me.DGTBPedidoReferencia.MappingName = "PedidoReferencia"
        Me.DGTBPedidoReferencia.Width = 75
        '
        'DGTBTipoPedido
        '
        Me.DGTBTipoPedido.Format = ""
        Me.DGTBTipoPedido.FormatInfo = Nothing
        Me.DGTBTipoPedido.HeaderText = "TipoPedido"
        Me.DGTBTipoPedido.MappingName = "TipoPedido"
        Me.DGTBTipoPedido.Width = 90
        '
        'DGTBTipoServicio
        '
        Me.DGTBTipoServicio.Format = ""
        Me.DGTBTipoServicio.FormatInfo = Nothing
        Me.DGTBTipoServicio.HeaderText = "TipoServicio"
        Me.DGTBTipoServicio.MappingName = "TipoServicio"
        Me.DGTBTipoServicio.Width = 150
        '
        'DGTBFCompromiso
        '
        Me.DGTBFCompromiso.Format = ""
        Me.DGTBFCompromiso.FormatInfo = Nothing
        Me.DGTBFCompromiso.HeaderText = "FCompromiso"
        Me.DGTBFCompromiso.MappingName = "FCompromiso"
        Me.DGTBFCompromiso.Width = 75
        '
        'DGTBTotal
        '
        Me.DGTBTotal.Format = "$####.##"
        Me.DGTBTotal.FormatInfo = Nothing
        Me.DGTBTotal.HeaderText = "Total"
        Me.DGTBTotal.MappingName = "Total"
        Me.DGTBTotal.Width = 75
        '
        'DGTBStatus
        '
        Me.DGTBStatus.Format = ""
        Me.DGTBStatus.FormatInfo = Nothing
        Me.DGTBStatus.HeaderText = "Status"
        Me.DGTBStatus.MappingName = "Status"
        Me.DGTBStatus.Width = 75
        '
        'DGTBCStatusServicioTecnico
        '
        Me.DGTBCStatusServicioTecnico.Format = ""
        Me.DGTBCStatusServicioTecnico.FormatInfo = Nothing
        Me.DGTBCStatusServicioTecnico.HeaderText = "StatusServicioTecnico"
        Me.DGTBCStatusServicioTecnico.MappingName = "StatusServicioTecnico"
        Me.DGTBCStatusServicioTecnico.Width = 75
        '
        'DGTBTipoCobro
        '
        Me.DGTBTipoCobro.Format = ""
        Me.DGTBTipoCobro.FormatInfo = Nothing
        Me.DGTBTipoCobro.HeaderText = "TipoCobro"
        Me.DGTBTipoCobro.MappingName = "TipoCobroDescripcion"
        Me.DGTBTipoCobro.Width = 75
        '
        'DGTBCFolio
        '
        Me.DGTBCFolio.Format = ""
        Me.DGTBCFolio.FormatInfo = Nothing
        Me.DGTBCFolio.HeaderText = "Folio"
        Me.DGTBCFolio.MappingName = "Folio"
        Me.DGTBCFolio.Width = 0
        '
        'DGTBCAñoAtt
        '
        Me.DGTBCAñoAtt.Format = ""
        Me.DGTBCAñoAtt.FormatInfo = Nothing
        Me.DGTBCAñoAtt.HeaderText = "AñoAtt"
        Me.DGTBCAñoAtt.MappingName = "AñoAtt"
        Me.DGTBCAñoAtt.Width = 0
        '
        'dtpLiquidacion
        '
        Me.dtpLiquidacion.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpLiquidacion.Location = New System.Drawing.Point(720, 8)
        Me.dtpLiquidacion.Name = "dtpLiquidacion"
        Me.dtpLiquidacion.Size = New System.Drawing.Size(136, 20)
        Me.dtpLiquidacion.TabIndex = 15
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblCliente, Me.lblCelula, Me.Label69, Me.Label71, Me.lblMunicipio, Me.lblCP, Me.lblColonia, Me.Label59, Me.Label61, Me.Label62, Me.lblNumeroInterior, Me.lblNumeroExterior, Me.lblCalle, Me.Label64, Me.Label65, Me.Label66, Me.lblRuta, Me.lblEmpresa, Me.lblNombre, Me.Label67, Me.Label68, Me.Label70})
        Me.GroupBox1.Location = New System.Drawing.Point(8, 264)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(440, 240)
        Me.GroupBox1.TabIndex = 331
        Me.GroupBox1.TabStop = False
        '
        'lblCliente
        '
        Me.lblCliente.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.Location = New System.Drawing.Point(80, 16)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(88, 21)
        Me.lblCliente.TabIndex = 356
        '
        'lblCelula
        '
        Me.lblCelula.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCelula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCelula.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCelula.Location = New System.Drawing.Point(232, 16)
        Me.lblCelula.Name = "lblCelula"
        Me.lblCelula.Size = New System.Drawing.Size(48, 21)
        Me.lblCelula.TabIndex = 355
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(184, 24)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(40, 13)
        Me.Label69.TabIndex = 354
        Me.Label69.Text = "Celula:"
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(8, 24)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(51, 13)
        Me.Label71.TabIndex = 353
        Me.Label71.Text = "Contrato:"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMunicipio
        '
        Me.lblMunicipio.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblMunicipio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMunicipio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMunicipio.Location = New System.Drawing.Point(232, 208)
        Me.lblMunicipio.Name = "lblMunicipio"
        Me.lblMunicipio.Size = New System.Drawing.Size(192, 21)
        Me.lblMunicipio.TabIndex = 352
        '
        'lblCP
        '
        Me.lblCP.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCP.Location = New System.Drawing.Point(80, 208)
        Me.lblCP.Name = "lblCP"
        Me.lblCP.Size = New System.Drawing.Size(80, 21)
        Me.lblCP.TabIndex = 351
        '
        'lblColonia
        '
        Me.lblColonia.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblColonia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblColonia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColonia.Location = New System.Drawing.Point(80, 176)
        Me.lblColonia.Name = "lblColonia"
        Me.lblColonia.Size = New System.Drawing.Size(344, 21)
        Me.lblColonia.TabIndex = 350
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(168, 216)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(55, 13)
        Me.Label59.TabIndex = 349
        Me.Label59.Text = "Municipio:"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(8, 216)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(23, 13)
        Me.Label61.TabIndex = 348
        Me.Label61.Text = "CP:"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(8, 184)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(46, 13)
        Me.Label62.TabIndex = 347
        Me.Label62.Text = "Colonia:"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNumeroInterior
        '
        Me.lblNumeroInterior.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNumeroInterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumeroInterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumeroInterior.Location = New System.Drawing.Point(272, 144)
        Me.lblNumeroInterior.Name = "lblNumeroInterior"
        Me.lblNumeroInterior.Size = New System.Drawing.Size(152, 21)
        Me.lblNumeroInterior.TabIndex = 346
        '
        'lblNumeroExterior
        '
        Me.lblNumeroExterior.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNumeroExterior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNumeroExterior.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumeroExterior.Location = New System.Drawing.Point(80, 144)
        Me.lblNumeroExterior.Name = "lblNumeroExterior"
        Me.lblNumeroExterior.Size = New System.Drawing.Size(112, 21)
        Me.lblNumeroExterior.TabIndex = 345
        '
        'lblCalle
        '
        Me.lblCalle.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblCalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCalle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalle.Location = New System.Drawing.Point(80, 112)
        Me.lblCalle.Name = "lblCalle"
        Me.lblCalle.Size = New System.Drawing.Size(344, 21)
        Me.lblCalle.TabIndex = 344
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(200, 152)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(49, 13)
        Me.Label64.TabIndex = 343
        Me.Label64.Text = "#Interior:"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(8, 152)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(53, 13)
        Me.Label65.TabIndex = 342
        Me.Label65.Text = "#Exterior:"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(8, 120)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(33, 13)
        Me.Label66.TabIndex = 341
        Me.Label66.Text = "Calle:"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRuta
        '
        Me.lblRuta.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuta.Location = New System.Drawing.Point(336, 16)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(88, 21)
        Me.lblRuta.TabIndex = 340
        '
        'lblEmpresa
        '
        Me.lblEmpresa.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmpresa.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpresa.Location = New System.Drawing.Point(80, 80)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.Size = New System.Drawing.Size(344, 21)
        Me.lblEmpresa.TabIndex = 339
        '
        'lblNombre
        '
        Me.lblNombre.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblNombre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNombre.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombre.Location = New System.Drawing.Point(80, 48)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(344, 21)
        Me.lblNombre.TabIndex = 338
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(8, 88)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(53, 13)
        Me.Label67.TabIndex = 337
        Me.Label67.Text = "Empresa:"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(8, 56)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(48, 13)
        Me.Label68.TabIndex = 336
        Me.Label68.Text = "Nombre:"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(288, 24)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(31, 13)
        Me.Label70.TabIndex = 335
        Me.Label70.Text = "Ruta:"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblSumaParcialidad, Me.lblAyudante, Me.lblTecnico, Me.Label42, Me.Label39, Me.lblUnidad, Me.Label41, Me.txtTrabajoRealizado, Me.Label1})
        Me.GroupBox2.Location = New System.Drawing.Point(456, 264)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(400, 240)
        Me.GroupBox2.TabIndex = 332
        Me.GroupBox2.TabStop = False
        '
        'lblSumaParcialidad
        '
        Me.lblSumaParcialidad.Location = New System.Drawing.Point(112, 16)
        Me.lblSumaParcialidad.Name = "lblSumaParcialidad"
        Me.lblSumaParcialidad.Size = New System.Drawing.Size(48, 24)
        Me.lblSumaParcialidad.TabIndex = 339
        '
        'lblAyudante
        '
        Me.lblAyudante.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblAyudante.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAyudante.Location = New System.Drawing.Point(80, 185)
        Me.lblAyudante.Name = "lblAyudante"
        Me.lblAyudante.Size = New System.Drawing.Size(304, 21)
        Me.lblAyudante.TabIndex = 338
        Me.lblAyudante.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTecnico
        '
        Me.lblTecnico.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblTecnico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTecnico.Location = New System.Drawing.Point(80, 153)
        Me.lblTecnico.Name = "lblTecnico"
        Me.lblTecnico.Size = New System.Drawing.Size(304, 21)
        Me.lblTecnico.TabIndex = 337
        Me.lblTecnico.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(8, 193)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(55, 14)
        Me.Label42.TabIndex = 336
        Me.Label42.Text = "Ayudante:"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(8, 161)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(46, 14)
        Me.Label39.TabIndex = 335
        Me.Label39.Text = "Técnico:"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblUnidad
        '
        Me.lblUnidad.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.lblUnidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUnidad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnidad.Location = New System.Drawing.Point(80, 121)
        Me.lblUnidad.Name = "lblUnidad"
        Me.lblUnidad.Size = New System.Drawing.Size(120, 21)
        Me.lblUnidad.TabIndex = 334
        Me.lblUnidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(8, 129)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(39, 14)
        Me.Label41.TabIndex = 333
        Me.Label41.Text = "Unidad"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTrabajoRealizado
        '
        Me.txtTrabajoRealizado.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtTrabajoRealizado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTrabajoRealizado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrabajoRealizado.Location = New System.Drawing.Point(8, 57)
        Me.txtTrabajoRealizado.Multiline = True
        Me.txtTrabajoRealizado.Name = "txtTrabajoRealizado"
        Me.txtTrabajoRealizado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTrabajoRealizado.Size = New System.Drawing.Size(384, 56)
        Me.txtTrabajoRealizado.TabIndex = 332
        Me.txtTrabajoRealizado.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 13)
        Me.Label1.TabIndex = 331
        Me.Label1.Text = "Trabajo Solicitado"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(608, 544)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 24)
        Me.Label2.TabIndex = 333
        Me.Label2.Text = "Créditos:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(608, 584)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 24)
        Me.Label3.TabIndex = 334
        Me.Label3.Text = "Contados:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(608, 624)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 24)
        Me.Label4.TabIndex = 335
        Me.Label4.Text = "Total:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCreditos
        '
        Me.lblCreditos.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreditos.ForeColor = System.Drawing.Color.Blue
        Me.lblCreditos.Location = New System.Drawing.Point(736, 544)
        Me.lblCreditos.Name = "lblCreditos"
        Me.lblCreditos.Size = New System.Drawing.Size(112, 24)
        Me.lblCreditos.TabIndex = 336
        Me.lblCreditos.Text = "0.0"
        Me.lblCreditos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblContados
        '
        Me.lblContados.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContados.ForeColor = System.Drawing.Color.Blue
        Me.lblContados.Location = New System.Drawing.Point(736, 584)
        Me.lblContados.Name = "lblContados"
        Me.lblContados.Size = New System.Drawing.Size(112, 24)
        Me.lblContados.TabIndex = 337
        Me.lblContados.Text = "0.0"
        Me.lblContados.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotal
        '
        Me.lblTotal.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.Color.Blue
        Me.lblTotal.Location = New System.Drawing.Point(736, 624)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(112, 24)
        Me.lblTotal.TabIndex = 338
        Me.lblTotal.Text = "0.0"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(712, 584)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 24)
        Me.Label5.TabIndex = 339
        Me.Label5.Text = "$"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(712, 544)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(24, 24)
        Me.Label6.TabIndex = 340
        Me.Label6.Text = "$"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(712, 624)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 24)
        Me.Label7.TabIndex = 341
        Me.Label7.Text = "$"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grdCheques
        '
        Me.grdCheques.CaptionText = "Cheques Capturados"
        Me.grdCheques.DataMember = ""
        Me.grdCheques.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdCheques.Location = New System.Drawing.Point(8, 512)
        Me.grdCheques.Name = "grdCheques"
        Me.grdCheques.ReadOnly = True
        Me.grdCheques.Size = New System.Drawing.Size(440, 144)
        Me.grdCheques.TabIndex = 342
        Me.grdCheques.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.grdCheques
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DGTBCNumeroCheque, Me.DGTBCNumeroCuenta, Me.DGTBCPedido, Me.DGTBCMonto, Me.DGTBCBanco, Me.DGTBCDisponible, Me.DGTBCCobro, Me.DGTBCAñoCobro})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "Cheque"
        '
        'DGTBCNumeroCheque
        '
        Me.DGTBCNumeroCheque.Format = ""
        Me.DGTBCNumeroCheque.FormatInfo = Nothing
        Me.DGTBCNumeroCheque.HeaderText = "NúmCheque"
        Me.DGTBCNumeroCheque.MappingName = "NumeroCheque"
        Me.DGTBCNumeroCheque.Width = 70
        '
        'DGTBCNumeroCuenta
        '
        Me.DGTBCNumeroCuenta.Format = ""
        Me.DGTBCNumeroCuenta.FormatInfo = Nothing
        Me.DGTBCNumeroCuenta.HeaderText = "NúmCuenta"
        Me.DGTBCNumeroCuenta.MappingName = "NumeroCuenta"
        Me.DGTBCNumeroCuenta.Width = 75
        '
        'DGTBCPedido
        '
        Me.DGTBCPedido.Format = ""
        Me.DGTBCPedido.FormatInfo = Nothing
        Me.DGTBCPedido.HeaderText = "Pedido"
        Me.DGTBCPedido.MappingName = "Pedido"
        Me.DGTBCPedido.Width = 75
        '
        'DGTBCMonto
        '
        Me.DGTBCMonto.Format = ""
        Me.DGTBCMonto.FormatInfo = Nothing
        Me.DGTBCMonto.HeaderText = "Monto"
        Me.DGTBCMonto.MappingName = "Monto"
        Me.DGTBCMonto.Width = 55
        '
        'DGTBCBanco
        '
        Me.DGTBCBanco.Format = ""
        Me.DGTBCBanco.FormatInfo = Nothing
        Me.DGTBCBanco.HeaderText = "Banco"
        Me.DGTBCBanco.MappingName = "Banco"
        Me.DGTBCBanco.Width = 120
        '
        'DGTBCDisponible
        '
        Me.DGTBCDisponible.Format = ""
        Me.DGTBCDisponible.FormatInfo = Nothing
        Me.DGTBCDisponible.HeaderText = "Disponible"
        Me.DGTBCDisponible.MappingName = "Disponible"
        Me.DGTBCDisponible.Width = 55
        '
        'DGTBCCobro
        '
        Me.DGTBCCobro.Format = ""
        Me.DGTBCCobro.FormatInfo = Nothing
        Me.DGTBCCobro.HeaderText = "Cobro"
        Me.DGTBCCobro.MappingName = "Cobro"
        Me.DGTBCCobro.Width = 0
        '
        'DGTBCAñoCobro
        '
        Me.DGTBCAñoCobro.Format = ""
        Me.DGTBCAñoCobro.FormatInfo = Nothing
        Me.DGTBCAñoCobro.HeaderText = "AñoCobro"
        Me.DGTBCAñoCobro.MappingName = "AñoCobro"
        Me.DGTBCAñoCobro.Width = 0
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(472, 7)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 23)
        Me.Label8.TabIndex = 343
        Me.Label8.Text = "Camioneta:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(648, 8)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 23)
        Me.Label9.TabIndex = 344
        Me.Label9.Text = "FLiquidación:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtKilometrajeInicial
        '
        Me.txtKilometrajeInicial.Location = New System.Drawing.Point(464, 560)
        Me.txtKilometrajeInicial.Name = "txtKilometrajeInicial"
        Me.txtKilometrajeInicial.Size = New System.Drawing.Size(120, 20)
        Me.txtKilometrajeInicial.TabIndex = 345
        Me.txtKilometrajeInicial.Text = ""
        '
        'txtKilometrajeFinal
        '
        Me.txtKilometrajeFinal.Location = New System.Drawing.Point(464, 624)
        Me.txtKilometrajeFinal.Name = "txtKilometrajeFinal"
        Me.txtKilometrajeFinal.Size = New System.Drawing.Size(120, 20)
        Me.txtKilometrajeFinal.TabIndex = 346
        Me.txtKilometrajeFinal.Text = ""
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(464, 528)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(100, 24)
        Me.Label10.TabIndex = 348
        Me.Label10.Text = "Kilometraje Inicial:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(464, 592)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(100, 24)
        Me.Label11.TabIndex = 349
        Me.Label11.Text = "Kilometraje Final:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmSelecLiquidacion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(866, 664)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label11, Me.Label10, Me.txtKilometrajeFinal, Me.txtKilometrajeInicial, Me.Label9, Me.Label8, Me.grdCheques, Me.Label7, Me.Label6, Me.Label5, Me.lblTotal, Me.lblContados, Me.lblCreditos, Me.Label4, Me.Label3, Me.Label2, Me.GroupBox2, Me.GroupBox1, Me.dtpLiquidacion, Me.grdLiquidacion, Me.cboLiquidacion, Me.tbLiquidacion})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSelecLiquidacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Liquidación Servicios Tecnicos"
        CType(Me.grdLiquidacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdCheques, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frmSelecLiquidacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LlenaGrid()
        If grdLiquidacion.VisibleRowCount > 0 Then
            Folio = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 9), Integer)
            AñoAtt = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 10), Integer)
        Else
        End If

        llenacombo()
        LlenaServicio()
        SumaCreditos()
        SumaContados()
        SumaTotal()
        llenaCheque()
        'VerificaCobro()
        lblSumaParcialidad.Visible = False
        StatusServicioTec()
    End Sub

    Private Sub tbLiquidacion_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tbLiquidacion.ButtonClick
        Select Case e.Button.Text
            Case "Aceptar"
                VerificaAtendidos()
                StatusFolio()
                SumaKilometraje()
                If StatusLogistica = "LIQCAJA" Then
                    MessageBox.Show("Este folio ya fue liquidado, revise su asignación e intente nuevamente", "Servicio Técnico", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    If Status = "ACTIVO" Then
                        MessageBox.Show("Usted tiene servicios con status ACTIVO. Cierre las ordenes faltantes", "Servicio Técnico", MessageBoxButtons.OK)
                    Else
                        If MessageBox.Show("¿Cerrar liquidación?.", "Servicios Tecnicos", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            Dim Conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                            Conexion.Open()
                            Dim Comando As New SqlCommand()
                            Dim Transaccion As SqlTransaction
                            If lblCreditos.Text = "" Then
                                Comando.Parameters.Add("@ImporteCredito", SqlDbType.Money).Value = 0
                            Else
                                Comando.Parameters.Add("@ImporteCredito", SqlDbType.Money).Value = lblCreditos.Text
                            End If
                            If lblContados.Text = "" Then
                                Comando.Parameters.Add("@ImporteContado", SqlDbType.Money).Value = 0
                            Else
                                Comando.Parameters.Add("@ImporteContado", SqlDbType.Money).Value = lblContados.Text
                            End If
                            Comando.Parameters.Add("@FLiquidacion", SqlDbType.DateTime).Value = dtpLiquidacion.Value
                            Comando.Parameters.Add("@UsuarioLiquidacion", SqlDbType.Char).Value = _Usuario
                            Comando.Parameters.Add("@Folio", SqlDbType.Int).Value = Folio
                            Comando.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = AñoAtt
                            Comando.Parameters.Add("@KilometrajeInicial", SqlDbType.Int).Value = txtKilometrajeInicial.Text
                            Comando.Parameters.Add("@KilometrajeFinal", SqlDbType.Int).Value = txtKilometrajeFinal.Text
                            Comando.Parameters.Add("@diferencia", SqlDbType.Int).Value = Diferencia

                            Transaccion = Conexion.BeginTransaction
                            Comando.Connection = Conexion
                            Comando.Transaction = Transaccion
                            Try
                                Comando.CommandType = CommandType.StoredProcedure
                                Comando.CommandText = "spSTCierraLiquidacionServicioTecnico"
                                Comando.CommandTimeout = 300
                                Comando.ExecuteNonQuery()
                                Transaccion.Commit()
                            Catch Ex As Exception
                                Transaccion.Rollback()
                                MessageBox.Show(Ex.Message)
                            Finally
                                Conexion.Close()
                                'Conexion.Dispose()
                                Me.Close()
                            End Try

                            Try
                                Dim Reportes As New frmReporte(dtpLiquidacion.Value, CType(lblCelula.Text, Integer), Folio)
                                Reportes.Imprime = 3
                                Reportes.Autotanque = CType(cboLiquidacion.Text, Integer)
                                Reportes.ShowDialog()
                            Catch er As Exception
                                MessageBox.Show("Error en la impresion del reporte de programacion" & er.Message & er.Source)

                            End Try
                        Else
                        End If
                    End If
                End If

            Case "Cerrar Orden"
                StatusServicioTec()
                If StatusServicioTecnico = "ATENDIDO" Then
                    MessageBox.Show("El servicio ya ha sido cerrado, por favor seleccione otro", "Servicio Técnico", MessageBoxButtons.OK)
                Else
                    Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    LlenaGrid()
                    SumaCreditos()
                    SumaContados()
                    SumaParcialidad()
                    SumaTotal()
                    Cursor = Cursors.Default
                End If

            Case "Act.Orden"
                If MessageBox.Show("¿Esta seguro de querer activar la orden de servicio técnico?", "Servicio técnico", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    Dim conexion As SqlConnection = SigaMetClasses.DataLayer.Conexion
                    conexion.Open()
                    Dim command As New SqlCommand()
                    Dim transaction As SqlTransaction
                    command.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
                    command.Parameters.Add("@Celula", SqlDbType.Int).Value = Celula
                    command.Parameters.Add("@AñoPed", SqlDbType.Int).Value = AñoPed
                    command.Parameters.Add("@Cobro", SqlDbType.Int).Value = Cobro
                    command.Parameters.Add("@AñoCobro", SqlDbType.Int).Value = AñoCobro
                    command.Parameters.Add("@TipoCobro", SqlDbType.Int).Value = TipoCobro
                    transaction = conexion.BeginTransaction
                    command.Connection = conexion
                    command.Transaction = transaction
                    Try
                        command.CommandType = CommandType.StoredProcedure
                        command.CommandText = "spSTActivaOrdenServicioTecnico"
                        command.ExecuteNonQuery()
                        transaction.Commit()
                    Catch Ex As Exception
                        transaction.Rollback()
                        MessageBox.Show(Ex.Message)
                    Finally
                        conexion.Close()
                        'conexion.Dispose()
                    End Try
                    LlenaGrid()
                Else
                End If

            Case "Cheques"
                StatusServicioTec()
                If StatusServicioTecnico = "ATENDIDO" Then
                    MessageBox.Show("El servicio técnico ya a sido ATENDIDO, no puede agregarle un cheque.", "Servicio Técnico", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                Else
                    Cursor = Cursors.WaitCursor
                    VerificaTipoCobro()
                    If TipoCobro = 5 Then
                        ExisteCheque()
                        If CType(NumCheque, Double) <> 0 Then

                            'Dim Cheques As New frmCheque(_Usuario, Pedido, Celula, AñoPed, TotalCliente, dtpLiquidacion.Value, CType(cboLiquidacion.SelectedValue, Integer), Cobro, AñoCobro)
                            'Cheques.txtCliente.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 0), String)
                            'cheques.btnAceptar.Enabled = False
                            'MessageBox.Show("El pedido " + CType(Pedido, String) + ". Tiene asignado el cheque " + CType(NumCheque, String) + ". No puede asignarle otro cheque", "Servicios Técnicos", MessageBoxButtons.OK)
                            'Cheques.ShowDialog()

                        Else
                            Cobro = 0
                            AñoCobro = 0
                            'Dim Cheques As New frmCheque(_Usuario, Pedido, Celula, AñoPed, TotalCliente, dtpLiquidacion.Value, CType(cboLiquidacion.SelectedValue, Integer), Cobro, AñoCobro)
                            'Cheques.txtCliente.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 0), String)
                            'cheques.btnCancelarCheque.Enabled = False
                            'Cheques.ShowDialog()
                        End If

                    Else

                        If MessageBox.Show("El pedido " + CType(Pedido, String) + " no es de contado, no puede tener cheques capturados.", "Servicio Técnicos", MessageBoxButtons.OK) = DialogResult.OK Then
                        End If
                    End If
                End If

                LlenaGrid()
                llenaCheque()
                Cursor = Cursors.Default

            Case "CancelarCheque"
                Cursor = Cursors.WaitCursor
                'ExisteCheque()
                Pedido = 0
                'Dim CancelaCheque As New frmCheque(_Usuario, Pedido, Celula, AñoPed, TotalCliente, dtpLiquidacion.Value, CType(cboLiquidacion.SelectedValue, Integer), Cobro, AñoCobro)
                'CancelaCheque.txtCliente.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 0), String)
                'CancelaCheque.btnAceptar.Enabled = False
                ''CancelaCheque.btnModificar.Enabled = False
                'CancelaCheque.ShowDialog()
                llenaCheque()
                LlenaGrid()
                SumaCreditos()
                SumaContados()
                SumaParcialidad()
                SumaTotal()
                Cursor = Cursors.Default
            Case "Refrescar"
                LlenaGrid()
                SumaCreditos()
                SumaContados()
                SumaTotal()
            Case "Cerrar"
                If MessageBox.Show("¿Desea salir de la Liquidación?", "Servicios Técnicos", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Else
                    Me.Close()
                End If
        End Select
    End Sub

    Private Sub cboLiquidacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLiquidacion.SelectedIndexChanged
        LlenaGrid()
        SumaCreditos()
        SumaContados()
        SumaParcialidad()
        SumaTotal()
        llenaCheque()
        VerificaTipoCobro()
    End Sub


    Private Sub dtpLiquidacion_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpLiquidacion.ValueChanged
        LlenaGrid()
        SumaCreditos()
        SumaContados()
        SumaParcialidad()
        SumaTotal()
        llenaCheque()
        'VerificaCobro()
    End Sub

    Private Sub Label70_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub grdLiquidacion_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLiquidacion.CurrentCellChanged
        PedidoReferencia = RTrim(CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 1), String))
        lblCliente.Text = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 0), String)
        Folio = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 9), Integer)
        AñoAtt = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 10), Integer)
        LlenaDetalle()
        'VerificaTipoCobro()
        'VerificaCobro()
        LlenaServicio()
        VerificaTipoCobro()
        'NoRepetirCobro()
        VerificaCobro()
        VerificaCheque()

    End Sub

    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub grdCheques_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCheques.CurrentCellChanged
        Cobro = CType(grdCheques.Item(grdCheques.CurrentRowIndex, 6), Integer)
        AñoCobro = CType(grdCheques.Item(grdCheques.CurrentRowIndex, 7), Integer)
        'DatosCheque()
    End Sub




End Class
