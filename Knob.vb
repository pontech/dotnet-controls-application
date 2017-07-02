Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Drawing

Public Class Knob
    Inherits PictureBox

    <Category("Data"), Description("Raised when value has changed.")>
    Public Event OnChange(ByVal Value As Single)

    Private _Minimum As Single = 0.0
    Private _Maximum As Single = 180.0
    Private _Value As Single
    Private _Increment As Single = 1.0
    Private _y_last As Integer
    Private _x_last As Integer
    Private _angle As Single
    Private _angle_last As Single
    Private _angle_delta As Single

    Public BrushKnobColor As SolidBrush
    Public BrushIndicatorColor As SolidBrush
    Public PenMouseLineColor As Pen

    Private KnobCenter As Point
    Private IndicatorRadius As Single


    Private _endColor As Color = Color.LimeGreen

    ' The Category attribute tells the designer to display it in the Flash grouping.   
    ' The Description attribute provides a description of the property.   
    <Category("Data"), Description("Get or set the value of the knob.")>
    Public Property Value() As Single
        Get
            Return _Value
        End Get
        Set
            _Value = Value
            RaiseEvent OnChange(Value)
            ' The Invalidate method calls the OnPaint method, which redraws the control.  
            Invalidate()
        End Set
    End Property

    <Category("Data"), Description("Get or set the minimum value of the knob.")>
    Public Property Minimum() As Single
        Get
            Return _Minimum
        End Get
        Set
            _Minimum = Value
            ' The Invalidate method calls the OnPaint method, which redraws the control.  
            Invalidate()
        End Set
    End Property

    <Category("Data"), Description("Get or set the maximum value of the knob.")>
    Public Property Maximum() As Single
        Get
            Return _Maximum
        End Get
        Set
            _Maximum = Value
            ' The Invalidate method calls the OnPaint method, which redraws the control.  
            Invalidate()
        End Set
    End Property

    <Category("Data"), Description("Get or set the increment value of the knob.")>
    Public Property Increment() As Single
        Get
            Return _Increment
        End Get
        Set
            _Increment = Value
            ' The Invalidate method calls the OnPaint method, which redraws the control.  
            Invalidate()
        End Set
    End Property

    Sub New()
        'SetStyle()
        BrushKnobColor = New SolidBrush(Color.Gray)
        BrushIndicatorColor = New SolidBrush(Color.Black)
        PenMouseLineColor = New Pen(Color.Red)

        KnobCenter = New Point()
        IndicatorRadius = 5.0
    End Sub

    Protected Overrides Sub OnPaint(pe As PaintEventArgs)
        Dim xoffset As Single = Math.Sin(_Value / _Maximum * Math.PI * 2) * (Me.Width / 2 - IndicatorRadius)
        Dim yoffset As Single = -Math.Cos(_Value / _Maximum * Math.PI * 2) * (Me.Height / 2 - IndicatorRadius)

        MyBase.OnPaint(pe)
        ' Draw Knob
        pe.Graphics.FillEllipse(BrushKnobColor, 0, 0, Me.Width, Me.Height)
        ' Draw Indicator
        pe.Graphics.FillEllipse(BrushIndicatorColor, KnobCenter.X - IndicatorRadius + xoffset, KnobCenter.Y - IndicatorRadius + yoffset, IndicatorRadius * 2, IndicatorRadius * 2)

        ' Draw line from control center to mouse xy
        pe.Graphics.DrawLine(PenMouseLineColor, KnobCenter.X, KnobCenter.Y, _x_last, _y_last)

    End Sub

    Private Sub twist_wheel(ByVal direction As Integer)
        'Debug.Print(e.Delta)
        If direction > 0 Then
            _Value += _Increment
        Else
            _Value -= _Increment
        End If
        'Debug.Print(Value)
        If _Value > _Maximum Then Value = _Maximum
        If _Value < _Minimum Then Value = _Minimum
        Value = _Value
        'Debug.Print(Value)
        'Me.Invalidate()
    End Sub

    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        MyBase.OnMouseWheel(e)
        twist_wheel(e.Delta)
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        Dim Delta_y As Integer
        MyBase.OnMouseMove(e)

        Delta_y = _y_last - e.Y

        'KnobCenter.X, KnobCenter.Y
        Dim o As Single = e.X - KnobCenter.X
        Dim a As Single = e.Y - KnobCenter.Y
        _angle = Math.Atan(o / a)

        o = _x_last - KnobCenter.X
        a = _y_last - KnobCenter.Y
        _angle_last = Math.Atan(o / a)

        _angle *= 180 / Math.PI
        _angle_last *= 180 / Math.PI

        _angle_delta = (_angle_last - _angle)


        If (e.Button = MouseButtons.Left) Then
            'twist_wheel(Delta_y)
            twist_wheel(_angle_delta)
        End If

        Debug.Print(_angle_last.ToString("0.00") + ", " + _angle.ToString("0.00") + ", " + _angle_delta.ToString("0.00"))

        _y_last = e.Y
        _x_last = e.X
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        KnobCenter.X = Me.Width / 2
        KnobCenter.Y = Me.Height / 2
    End Sub

End Class
