
' Lite Process Monitor


Option Strict On

''' <summary>
''' Repr�sente l'arborescence des d�pendances d'un ex�cutable
''' </summary>
''' <remarks></remarks>
Public Class NativeDependenciesTree
    ''' <summary>
    ''' Une dll et ses d�pendences
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NativeDependency
        'liste des dll d�j� trouv�es
        Private Shared Cache As New Dictionary(Of String, NativeDependency)

        Private m_PE As PEFile
        ''' <summary>
        ''' Renvoie les informations du fichier PE
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PE() As PEFile
            Get
                Return m_PE
            End Get
        End Property

        Private m_Resolved As Boolean
        ''' <summary>
        ''' Indique si le dll a �t� trouv�e
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Resolved() As Boolean
            Get
                Return m_Resolved
            End Get
        End Property

        Private Declare Function LoadLibrary Lib "kernel32.dll" Alias "LoadLibraryA" (ByVal lpLibFileName As String) As IntPtr
        Private Declare Function FreeLibrary Lib "kernel32.dll" (ByVal hLibModule As IntPtr) As Integer
        Private Declare Function GetModuleFileName Lib "kernel32.dll" Alias "GetModuleFileNameA" (ByVal hModule As IntPtr, ByVal lpFileName As System.Text.StringBuilder, ByVal nSize As Integer) As Integer
        ''' <summary>
        ''' Renvoie le chemin complet d'une dll (en la chargeant et la d�chargeant)
        ''' </summary>
        ''' <param name="szFileName">nom relatif de la dll</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ResolveDll(ByVal szFileName As String) As String
            'charge la dll
            Dim hModule As IntPtr = LoadLibrary(szFileName)
            If (hModule.IsNotNull) Then
                'r�cup�re le nom complet de la dll
                Dim sbFileName As New System.Text.StringBuilder(1024)
                Dim retLen As Integer = GetModuleFileName(hModule, sbFileName, 1024)
                'lib�re la dll
                FreeLibrary(hModule)
                If retLen > 0 Then
                    Return sbFileName.ToString(0, retLen)
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Function

        Private m_FileName As String
        ''' <summary>
        ''' renvoie le nom de la dll
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FileName() As String
            Get
                Return m_FileName
            End Get
        End Property

        ''' <summary>
        ''' essaie de r�cup�rer les infos sur le fichier PE pass� en argument
        ''' </summary>
        ''' <param name="szFileName">nom de la dll (absolu ou relatif)</param>
        ''' <remarks></remarks>
        Friend Sub New(ByVal szFileName As String)
            Dim fFile As String = szFileName
            'si pas absolu
            If Not System.IO.Path.IsPathRooted(szFileName) Then
                'on essaie de r�soudre
                fFile = ResolveDll(szFileName)
            End If

            m_FileName = szFileName

            If fFile IsNot Nothing AndAlso System.IO.File.Exists(fFile) Then
                'si on a toruv�, on r�cup�re les infos
                Me.m_Resolved = True
                m_PE = New PEFile(fFile)
            Else
                Me.m_Resolved = False
            End If
        End Sub

        Private m_Dependencies As List(Of NativeDependency) = Nothing
        Public ReadOnly Property Dependencies() As IEnumerable(Of NativeDependency)
            Get
                'r�cup�re les infos sur la liste des d�pendances "� la demande"
                If Me.PE IsNot Nothing AndAlso m_Dependencies Is Nothing Then
                    m_Dependencies = New List(Of NativeDependency)
                    'pour chaque dll import�e
                    For Each i As DllImportEntry In Me.PE.ImportDirectory.DllEntries
                        'si pas d�j� rencontr�, on l'ajoute au cache des "trouv�es"
                        If Not Cache.ContainsKey(i.DllName) Then
                            Cache.Add(i.DllName, New NativeDependency(i.DllName))
                        End If
                        Me.m_Dependencies.Add(Cache(i.DllName))
                    Next
                End If
                Return m_Dependencies
            End Get
        End Property
    End Class

    Private m_MainDll As NativeDependency
    ''' <summary>
    ''' Dll racine de l'arborescence repr�sent�e
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MainDll() As NativeDependency
        Get
            Return m_MainDll
        End Get
    End Property

    ''' <summary>
    ''' R�cup�re les d�pendences d'un ex�cutable
    ''' </summary>
    ''' <param name="szFileName"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal szFileName As String)
        Me.m_MainDll = New NativeDependency(szFileName)
    End Sub

    ''' <summary>
    ''' R�cup�re r�cursivement les d�pendances
    ''' </summary>
    ''' <param name="allDeps">liste de toutes le d�pendances</param>
    ''' <param name="dep">d�pendance � traiter</param>
    ''' <remarks></remarks>
    Private Sub ProcessDependencies(ByVal allDeps As List(Of NativeDependency), ByVal dep As NativeDependency)
        'ajoute la dll � la liste des d�pendances
        allDeps.Add(dep)
        'si on a toruv� la dll
        If dep.Resolved Then
            'pour chaque d�pendance direct
            For Each natDep As NativeDependency In dep.Dependencies
                'si l'on n'a pas d�j� la dll dans la liste
                If Not allDeps.Contains(natDep) Then
                    Me.ProcessDependencies(allDeps, natDep)
                End If
            Next
        End If
    End Sub
    ''' <summary>
    ''' R�cup�re l'ensemble des d�pendances d'un ex�cutable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllDependencies() As IEnumerable(Of NativeDependency)
        Dim l As New List(Of NativeDependency)
        Me.ProcessDependencies(l, Me.MainDll)
        Return l
    End Function
End Class
