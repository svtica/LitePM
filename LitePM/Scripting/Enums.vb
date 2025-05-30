
' Lite Process Monitor









'




'



Option Strict On

Namespace Scripting

    Public Class Enums

        ' Operators
        Public Enum [Operator]
            eq          ' =
            neq         ' <>
            gt          ' >
            lt          ' <
            goe         ' >=
            loe         ' <=
            cont        ' contains (for a string)
        End Enum

        ' Objects
        Public Enum [Object]
            Process
            Service
        End Enum

        ' Condition
        Public Enum Condition
            Name
            Pid
        End Enum

        ' Machine types
        Public Enum MachineType
            Local
            Wmi
        End Enum

        ' Process operation
        Public Enum ProcessOperation
            Kill
            KillTree
            Pause
            [Resume]
            SetPriority     ' arg1 (0 (idle), ..., 5 (real time))
            SetAffinity     ' arg1 (1,2...)
        End Enum

        ' Service operation
        Public Enum ServiceOperation
            Start
            [Stop]
            Delete
            Pause
            [Resume]
            SetStartType    ' arg1 (0 (disabled), 1 (on demand), 2 (auto))
        End Enum

    End Class

End Namespace
