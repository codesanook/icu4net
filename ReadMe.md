ICU4NET provides binding for .NET language over ICU4C.

Currently, we focus on implementing only the boundary analysis part.

## How to use
Just install NuGet package [`ICU4NET`][icu4net-nuget].

	Install-Package ICU4NET

## Boundary Analysis / Word Breaking
This example shows how to perform word breaking on a given string and get the result in enumerable form. Enumerate is implemented as C# Extension Method of BreakIterator, and requires ICU4NETExtension to be imported.

	// using ICU4NET;
	// using ICU4NETExtension;

	using (BreakIterator bi = BreakIterator.CreateWordInstance(Locale.GetUS()))
	{
		bi.SetText(uxText.Text);

		// requires ICU4NETExtension to use Enumerate extension method
		var words = bi.Enumerate().ToList(); 
		uxText.Text = string.Join("-", words.ToArray());
	}

By providing this IEnumerable interface, ICU4NETExtension allows you to do something like this:

	// using ICU4NET;
	// using ICU4NETExtension;

	using (BreakIterator bi = BreakIterator.CreateWordInstance(Locale.GetUS()))
	{
		bi.SetText(uxText.Text);

		// requires ICU4NETExtension to use Enumerate extension method
		MessageBox.Show(string.Join(Environment.NewLine, bi.Enumerate()
			.GroupBy(w => w)
			.OrderBy(x => x.Count())
			.Reverse()
			.Select(x => x.Key + " : " + x.Count())
			.Take(10)
			.ToArray()));
	}

This another example shows code which produces the same result, using the traditional way as used in original ICU4C/ICU4J and their documentation

	private List<string> WordBreak(string text)
	{
		var sb = new StringBuilder();
		var col = new List<string>();

		using (BreakIterator bi = BreakIterator.CreateWordInstance(Locale.GetUS()))
		{
			bi.SetText(text);
			int start = bi.First(), end = bi.Next();
			while (end != BreakIterator.DONE)
			{
				col.Add(text.Substring(start, end - start));
				start = end; end = bi.Next();
			}
		}

		return col;
	}

## Example Application
Features:

* Break input text into words
* List distinct words in text, sorted alphabetically
* List top ten words with most occurrence.
 
![screenshot](http://lh5.ggpht.com/_5XDoB4MglkY/S4_6QN_lq3I/AAAAAAAAFk0/LsUGhSGloIw/s800/WordBreak.png)

### Get DLL from 
https://sourceforge.net/projects/icu/files/ICU4C/51.2/

## License
Copyright (c) 2010 Natthawut Kulnirundorn

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

[icu4net-nuget]:https://www.nuget.org/packages/ICU4NET/
[icu-download]:http://site.icu-project.org/download
[icu-55x32]:http://download.icu-project.org/files/icu4c/55.1/icu4c-55_1-Win32-msvc10.zip