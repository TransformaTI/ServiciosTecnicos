Public Class AgregarEquipo
    Inherits System.Windows.Forms.UserControl
    Public _Cliente As Integer

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Cliente As Integer)
        MyBase.New()
        _Cliente = Cliente
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents btnAgregarEquipo As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(AgregarEquipo))
        Me.btnAgregarEquipo = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnAgregarEquipo
        '
        Me.btnAgregarEquipo.Image = CType(resources.GetObject("btnAgregarEquipo.Image"), System.Drawing.Bitmap)
        Me.btnAgregarEquipo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAgregarEquipo.Name = "btnAgregarEquipo"
        Me.btnAgregarEquipo.TabIndex = 0
        Me.btnAgregarEquipo.Text = "Agregar"
        Me.btnAgregarEquipo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AgregarEquipo
        '
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnAgregarEquipo})
        Me.Name = "AgregarEquipo"
        Me.Size = New System.Drawing.Size(80, 24)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnAgregarEquipo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarEquipo.Click
        Cursor = Cursors.WaitCursor
        Dim Agregar As New frmAgregaEquipo(_Cliente)
        Agregar.ShowDialog()
        Cursor = Cursors.Default
    End Sub
End Class
