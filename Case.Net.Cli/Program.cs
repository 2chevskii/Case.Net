using System.CommandLine;

namespace Case.Net.Cli;

public class Program
{
    public static int Main(string[] args)
    {
        var rootCommand = new RootCommand( "Case.Net command line interface" );

        var detectCommand = new Command( "detect", "Detect naming convention of a given string" );
        detectCommand.AddArgument(
            new Argument<string>( "input", "String to detect naming convention of" )
        );

        detectCommand.SetHandler(
            input => {
                /*detect naming convention*/
            },
            (Argument<string>) detectCommand.Arguments.First()
        );

        var convertCommand = new Command( "convert", "Convert given string to a given convention" );
        convertCommand.AddArgument( new Argument<string>( "input", "String to convert" ) );
        convertCommand.AddArgument(
            new Argument<string>( "convention name", "Target naming convention" )
        );

        convertCommand.SetHandler(
            (input, conventionName) => {
                /*convert string*/
            },
            (Argument<string>) convertCommand.Arguments.First(),
            (Argument<string>) convertCommand.Arguments.Last()
        );

        rootCommand.AddCommand( detectCommand );
        rootCommand.AddCommand( convertCommand );

        return rootCommand.Invoke( args );
    }
}
