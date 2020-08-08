using System;

using Cake.Core;
using Cake.Core.IO;
using Cake.SevenZip.Parsers;
using Cake.SevenZip.Switches;

namespace Cake.SevenZip.Commands
{
    /// <summary>
    /// Test one or more archives
    /// (Command: t).
    /// </summary>
    public sealed class TestCommand :
        OutputCommand<ITestOutput>,
        IHaveArgumentArchive,
        IHaveArgumentFiles,
        ISupportSwitchIncludeArchiveFilenames,
        ISupportSwitchExcludeArchiveFilenames,
        ISupportSwitchDisableParsingOfArchiveName,
        ISupportSwitchIncludeFilenames,
        ISupportSwitchExcludeFilenames,
        ISupportSwitchPassword,
        ISupportSwitchNtfsAlternateStreams,
        ISupportSwitchRecurseSubdirectories
    {
        private readonly TestOutputParser outputParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCommand"/> class.
        /// </summary>
        public TestCommand()
        {
            outputParser = new TestOutputParser();
        }

        /// <inheritdoc />
        public FilePath Archive { private get; set; }

        /// <inheritdoc />
        public SwitchIncludeArchiveFilenameCollection IncludeArchiveFilenames { get; set; }

        /// <inheritdoc />
        public SwitchExcludeArchiveFilenameCollection ExcludeArchiveFilenames { get; set; }

        /// <inheritdoc />
        public SwitchDisableParsingOfArchiveName DisableParsingOfArchiveName { get; set; }

        /// <inheritdoc />
        public SwitchIncludeFilenameCollection IncludeFilenames { get; set; }

        /// <inheritdoc />
        public SwitchExcludeFilenameCollection ExcludeFilenames { get; set; }

        /// <inheritdoc />
        public SwitchPassword Password { get; set; }

        /// <inheritdoc />
        public SwitchNtfsAlternateStreams NtfsAlternateStreams { get; set; }

        /// <inheritdoc />
        public SwitchRecurseSubdirectories RecurseSubdirectories { get; set; }

        /// <inheritdoc />
        public FilePathCollection Files { get; set; }

        /// <inheritdoc/>
        internal override IOutputParser<ITestOutput> OutputParser => outputParser;

        /// <inheritdoc/>
        public override void BuildArguments(ref ProcessArgumentBuilder builder)
        {
            if (Archive == null)
            {
                throw new ArgumentException("Archive is required for extract.");
            }

            builder.Append("t");
            builder.AppendQuoted(Archive.FullPath);

            if (Files != null)
            {
                foreach (var file in Files)
                {
                    builder.AppendQuoted(file.FullPath);
                }
            }

            foreach (var sw in new ISwitch[]
            {
                IncludeArchiveFilenames,
                ExcludeArchiveFilenames,
                DisableParsingOfArchiveName,
                IncludeFilenames,
                ExcludeFilenames,
                Password,
                NtfsAlternateStreams,
                RecurseSubdirectories,
            })
            {
                sw?.BuildArguments(ref builder);
            }
        }
    }
}