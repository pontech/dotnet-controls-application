Imports System.Windows.Forms
Imports System.Drawing

Public Class Knob
    Inherits PictureBox

    Public Minimum As Double = 0
    Public Maximum As Double = 180
    Public Value As Double
    Public Tick As Double = 1

    Public BrushKnobColor As SolidBrush
    Public BrushIndicatorColor As SolidBrush

    Private KnobCenter As Point
    Private IndicatorRadius As Single

    Sub New()
        'SetStyle()
        BrushKnobColor = New SolidBrush(Color.Gray)
        BrushIndicatorColor = New SolidBrush(Color.Black)

        KnobCenter = New Point()
        IndicatorRadius = 5.0
    End Sub

    Protected Overrides Sub OnPaint(pe As PaintEventArgs)
        Dim xoffset As Single = -Math.Sin(Value / Maximum * Math.PI * 2) * (Me.Width / 2 - IndicatorRadius)
        Dim yoffset As Single = Math.Cos(Value / Maximum * Math.PI * 2) * (Me.Height / 2 - IndicatorRadius)

        MyBase.OnPaint(pe)
        ' Draw Knob
        pe.Graphics.FillEllipse(BrushKnobColor, 0, 0, Me.Width, Me.Height)
        ' Draw Indicator
        pe.Graphics.FillEllipse(BrushIndicatorColor, KnobCenter.X - IndicatorRadius + xoffset, KnobCenter.Y - IndicatorRadius + yoffset, IndicatorRadius * 2, IndicatorRadius * 2)

    End Sub

    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        MyBase.OnMouseWheel(e)
        'Debug.Print(e.Delta)
        If e.Delta > 0 Then
            Value += Tick
        Else
            Value -= Tick
        End If
        'Debug.Print(Value)
        If Value > Maximum Then Value = Maximum
        If Value < Minimum Then Value = Minimum
        'Debug.Print(Value)
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        KnobCenter.X = Me.Width / 2
        KnobCenter.Y = Me.Height / 2
    End Sub

End Class
