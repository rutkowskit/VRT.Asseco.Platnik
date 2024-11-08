Imports VRT.Asseco.Platnik.Infos

Module Module1

    Sub Main()
        Dim tests As (Encoded As String, Decoded As String)() = {
           ("ux{w{snvtyxzqsppy{zomqlnprnlllyvzn{vxvxspnmvuq", "Aęćłśddfdksdu134j65858."),
           ("r{xzvpnwwxsmzwyuvrrvomrsnnslrlpv{rrwqxtmzorwwn", "~ąÓŁŹźŻżóó12345=+/\\//\")
       }

        Console.WriteLine("Testy dekodowania")
        For Each test In tests
            Dim encoded = test.Encoded
            Dim expected = test.Decoded
            Dim decoded = PlatnikAppInfo.DecodePassword(encoded)
            Dim status = If(String.Equals(expected, decoded), "OK", "NOTOK")
            Console.WriteLine("[{0}] Zakodowane: {1}, Odkodowane: {2}, Spodziewane: {3}", status, encoded, decoded, expected)
        Next

        Console.WriteLine()
        Console.WriteLine("Testy kodowania")
        For Each test In tests
            Dim expected = test.Encoded
            Dim decoded = test.Decoded
            Dim encoded = PlatnikAppInfo.EncodePassword(decoded)
            Dim status = If(String.Equals(expected, encoded), "OK", "NOTOK")
            Console.WriteLine("[{0}] Zakodowane: {1}, Odkodowane: {2}, Spodziewane: {3}", status, encoded, decoded, expected)
        Next
    End Sub

End Module
