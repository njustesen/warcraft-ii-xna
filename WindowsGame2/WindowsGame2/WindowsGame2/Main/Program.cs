using System;


namespace RTS {
#if WINDOWS || XBOX
    static class Program {
        static bool editorEnabled = false;
        static void Main(string[] args) {
            
            using (Game game = new Game()){
                if (!editorEnabled){
                    game.Run();
                }
            }
            using (Editor editor = new Editor()){
                if (editorEnabled){
                    editor.Run();
                }
            }
        }
    }
#endif
}
