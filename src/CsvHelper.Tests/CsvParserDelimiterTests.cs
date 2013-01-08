﻿using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvHelper.Tests
{
	[TestClass]
	public class CsvParserDelimiterTests
	{
		[TestMethod]
		public void DifferentDelimiterTest()
		{
			using( var stream = new MemoryStream() )
			using( var reader = new StreamReader( stream ) )
			using( var writer = new StreamWriter( stream ) )
			using( var parser = new CsvParser( reader ) )
			{
				writer.WriteLine( "1\t2\t3" );
				writer.WriteLine( "4\t5\t6" );
				writer.Flush();
				stream.Position = 0;

				parser.Configuration.Delimiter = "\t";
				parser.Configuration.HasHeaderRecord = false;

				var row = parser.Read();
				Assert.IsNotNull( row );
				Assert.AreEqual( 3, row.Length );
				Assert.AreEqual( "1", row[0] );
				Assert.AreEqual( "2", row[1] );
				Assert.AreEqual( "3", row[2] );

				row = parser.Read();
				Assert.IsNotNull( row );
				Assert.AreEqual( 3, row.Length );
				Assert.AreEqual( "4", row[0] );
				Assert.AreEqual( "5", row[1] );
				Assert.AreEqual( "6", row[2] );

				row = parser.Read();
				Assert.IsNull( row );
			}
		}

		[TestMethod]
		public void MultipleCharDelimiter2Test()
		{
			using( var stream = new MemoryStream() )
			using( var reader = new StreamReader( stream ) )
			using( var writer = new StreamWriter( stream ) )
			using( var parser = new CsvParser( reader ) )
			{
				writer.WriteLine( "1``2``3" );
				writer.WriteLine( "4``5``6" );
				writer.Flush();
				stream.Position = 0;

				parser.Configuration.Delimiter = "``";
				parser.Configuration.HasHeaderRecord = false;

				var row = parser.Read();
				Assert.IsNotNull( row );
				Assert.AreEqual( 3, row.Length );
				Assert.AreEqual( "1", row[0] );
				Assert.AreEqual( "2", row[1] );
				Assert.AreEqual( "3", row[2] );

				row = parser.Read();
				Assert.IsNotNull( row );
				Assert.AreEqual( 3, row.Length );
				Assert.AreEqual( "4", row[0] );
				Assert.AreEqual( "5", row[1] );
				Assert.AreEqual( "6", row[2] );

				row = parser.Read();
				Assert.IsNull( row );
			}
		}

		[TestMethod]
		public void MultipleCharDelimiter3Test()
		{
			using( var stream = new MemoryStream() )
			using( var reader = new StreamReader( stream ) )
			using( var writer = new StreamWriter( stream ) )
			using( var parser = new CsvParser( reader ) )
			{
				writer.WriteLine( "1`\t`2`\t`3" );
				writer.WriteLine( "4`\t`5`\t`6" );
				writer.Flush();
				stream.Position = 0;

				parser.Configuration.Delimiter = "`\t`";
				parser.Configuration.HasHeaderRecord = false;

				var row = parser.Read();
				Assert.IsNotNull( row );
				Assert.AreEqual( 3, row.Length );
				Assert.AreEqual( "1", row[0] );
				Assert.AreEqual( "2", row[1] );
				Assert.AreEqual( "3", row[2] );

				row = parser.Read();
				Assert.IsNotNull( row );
				Assert.AreEqual( 3, row.Length );
				Assert.AreEqual( "4", row[0] );
				Assert.AreEqual( "5", row[1] );
				Assert.AreEqual( "6", row[2] );

				row = parser.Read();
				Assert.IsNull( row );
			}
		}

		[TestMethod]
		public void AllFieldsEmptyTest()
		{
			using( var stream = new MemoryStream() )
			using( var reader = new StreamReader( stream ) )
			using( var writer = new StreamWriter( stream ) )
			using( var parser = new CsvParser( reader ) )
			{
				writer.WriteLine( ";;;;" );
				writer.WriteLine( ";;;;" );
				writer.Flush();
				stream.Position = 0;

				parser.Configuration.Delimiter = ";;";
				parser.Configuration.HasHeaderRecord = false;

				var row = parser.Read();
				Assert.IsNotNull( row );
				Assert.AreEqual( 3, row.Length );
				Assert.AreEqual( "", row[0] );
				Assert.AreEqual( "", row[1] );
				Assert.AreEqual( "", row[2] );

				row = parser.Read();
				Assert.IsNotNull( row );
				Assert.AreEqual( 3, row.Length );
				Assert.AreEqual( "", row[0] );
				Assert.AreEqual( "", row[1] );
				Assert.AreEqual( "", row[2] );

				row = parser.Read();
				Assert.IsNull( row );
			}
		}

		[TestMethod]
		public void AllFieldsEmptyNoEolOnLastLineTest()
		{
			using( var stream = new MemoryStream() )
			using( var reader = new StreamReader( stream ) )
			using( var writer = new StreamWriter( stream ) )
			using( var parser = new CsvParser( reader ) )
			{
				writer.WriteLine( ";;;;" );
				writer.Write( ";;;;" );
				writer.Flush();
				stream.Position = 0;

				parser.Configuration.Delimiter = ";;";
				parser.Configuration.HasHeaderRecord = false;

				var row = parser.Read();
				Assert.IsNotNull( row );
				Assert.AreEqual( 3, row.Length );
				Assert.AreEqual( "", row[0] );
				Assert.AreEqual( "", row[1] );
				Assert.AreEqual( "", row[2] );

				row = parser.Read();
				Assert.IsNotNull( row );
				Assert.AreEqual( 3, row.Length );
				Assert.AreEqual( "", row[0] );
				Assert.AreEqual( "", row[1] );
				Assert.AreEqual( "", row[2] );

				row = parser.Read();
				Assert.IsNull( row );
			}
		}

		[TestMethod]
		public void EmptyLastFieldTest()
		{
			using( var stream = new MemoryStream() )
			using( var reader = new StreamReader( stream ) )
			using( var writer = new StreamWriter( stream ) )
			using( var parser = new CsvParser( reader ) )
			{
				writer.WriteLine( "1;;2;;" );
				writer.WriteLine( "4;;5;;" );
				writer.Flush();
				stream.Position = 0;

				parser.Configuration.Delimiter = ";;";
				parser.Configuration.HasHeaderRecord = false;

				var row = parser.Read();
				Assert.IsNotNull( row );
				Assert.AreEqual( 3, row.Length );
				Assert.AreEqual( "1", row[0] );
				Assert.AreEqual( "2", row[1] );
				Assert.AreEqual( "", row[2] );

				row = parser.Read();
				Assert.IsNotNull( row );
				Assert.AreEqual( 3, row.Length );
				Assert.AreEqual( "4", row[0] );
				Assert.AreEqual( "5", row[1] );
				Assert.AreEqual( "", row[2] );

				row = parser.Read();
				Assert.IsNull( row );
			}
		}

		[TestMethod]
		public void EmptyLastFieldNoEolOnLastLineTest()
		{
			using( var stream = new MemoryStream() )
			using( var reader = new StreamReader( stream ) )
			using( var writer = new StreamWriter( stream ) )
			using( var parser = new CsvParser( reader ) )
			{
				writer.WriteLine( "1;;2;;" );
				writer.Write( "4;;5;;" );
				writer.Flush();
				stream.Position = 0;

				parser.Configuration.Delimiter = ";;";
				parser.Configuration.HasHeaderRecord = false;

				var row = parser.Read();
				Assert.IsNotNull( row );
				Assert.AreEqual( 3, row.Length );
				Assert.AreEqual( "1", row[0] );
				Assert.AreEqual( "2", row[1] );
				Assert.AreEqual( "", row[2] );

				row = parser.Read();
				Assert.IsNotNull( row );
				Assert.AreEqual( 3, row.Length );
				Assert.AreEqual( "4", row[0] );
				Assert.AreEqual( "5", row[1] );
				Assert.AreEqual( "", row[2] );

				row = parser.Read();
				Assert.IsNull( row );
			}
		}

		[TestMethod]
		public void DifferentDelimiter2ByteCountTest()
		{
			using( var stream = new MemoryStream() )
			using( var reader = new StreamReader( stream ) )
			using( var writer = new StreamWriter( stream ) )
			using( var parser = new CsvParser( reader ) )
			{
				writer.Write( "1;;2\r\n" );
				writer.Write( "4;;5\r\n" );
				writer.Flush();
				stream.Position = 0;

				parser.Configuration.Delimiter = ";;";
				parser.Configuration.HasHeaderRecord = false;
				parser.Configuration.CountBytes = true;

				parser.Read();
				Assert.AreEqual( 5, parser.BytePosition );

				parser.Read();
				Assert.AreEqual( 11, parser.BytePosition );

				parser.Read();
				Assert.AreEqual( 12, parser.BytePosition );
			}
		}

		[TestMethod]
		public void DifferentDelimiter3ByteCountTest()
		{
			using( var stream = new MemoryStream() )
			using( var reader = new StreamReader( stream ) )
			using( var writer = new StreamWriter( stream ) )
			using( var parser = new CsvParser( reader ) )
			{
				writer.Write( "1;;;2\r\n" );
				writer.Write( "4;;;5\r\n" );
				writer.Flush();
				stream.Position = 0;

				parser.Configuration.Delimiter = ";;;";
				parser.Configuration.HasHeaderRecord = false;
				parser.Configuration.CountBytes = true;

				parser.Read();
				Assert.AreEqual( 6, parser.BytePosition );

				parser.Read();
				Assert.AreEqual( 13, parser.BytePosition );

				parser.Read();
				Assert.AreEqual( 14, parser.BytePosition );
			}
		}
	}
}