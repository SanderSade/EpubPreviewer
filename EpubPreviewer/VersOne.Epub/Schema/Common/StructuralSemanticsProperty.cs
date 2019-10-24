namespace SanderSade.EpubPreviewer.VersOne.Epub.Schema.Common
{
	public enum StructuralSemanticsProperty
	{
		Cover = 1,
		Frontmatter,
		Bodymatter,
		Backmatter,
		Volume,
		Part,
		Chapter,
		Subchapter,
		Division,
		Abstract,
		Foreword,
		Preface,
		Prologue,
		Introduction,
		Preamble,
		Conclusion,
		Epilogue,
		Afterword,
		Epigraph,
		Toc,
		TocBrief,
		Landmarks,
		Loa,
		Loi,
		Lot,
		Lov,
		Appendix,
		Colophon,
		Credits,
		Keywords,
		Index,
		IndexHeadnotes,
		IndexLegend,
		IndexGroup,
		IndexEntryList,
		IndexEntry,
		IndexTerm,
		IndexEditorNote,
		IndexLocator,
		IndexLocatorList,
		IndexLocatorRange,
		IndexXrefPreferred,
		IndexXrefRelated,
		IndexTermCategory,
		IndexTermCategories,
		Glossary,
		Glossterm,
		Glossdef,
		Bibliography,
		Biblioentry,
		Titlepage,
		Halftitlepage,
		CopyrightPage,
		Seriespage,
		Acknowledgments,
		Imprint,
		Imprimatur,
		Contributors,
		OtherCredits,
		Errata,
		Dedication,
		RevisionHistory,
		CaseStudy,
		Help,
		Marginalia,
		Notice,
		Pullquote,
		Sidebar,
		Tip,
		Warning,
		Halftitle,
		Fulltitle,
		Covertitle,
		Title,
		Subtitle,
		Label,
		Ordinal,
		Bridgehead,
		LearningObjective,
		LearningObjectives,
		LearningOutcome,
		LearningOutcomes,
		LearningResource,
		LearningResources,
		LearningStandard,
		LearningStandards,
		Answer,
		Answers,
		Assessment,
		Assessments,
		Feedback,
		FillInTheBlankProblem,
		GeneralProblem,
		Qna,
		MatchProblem,
		MultipleChoiceProblem,
		Practice,
		Question,
		Practices,
		TrueFalseProblem,
		Panel,
		PanelGroup,
		Balloon,
		TextArea,
		SoundArea,
		Annotation,
		Note,
		Footnote,
		Endnote,
		Rearnote,
		Footnotes,
		Endnotes,
		Rearnotes,
		Annoref,
		Biblioref,
		Glossref,
		Noteref,
		Backlink,
		Credit,
		Keyword,
		TopicSentence,
		ConcludingSentence,
		Pagebreak,
		PageList,
		Table,
		TableRow,
		TableCell,
		List,
		ListItem,
		Figure,
		Unknown
	}

	internal static class StructuralSemanticsPropertyParser
	{
		public static StructuralSemanticsProperty Parse(string stringValue)
		{
			switch (stringValue.ToLowerInvariant())
			{
				case "cover":
					return StructuralSemanticsProperty.Cover;
				case "frontmatter":
					return StructuralSemanticsProperty.Frontmatter;
				case "bodymatter":
					return StructuralSemanticsProperty.Bodymatter;
				case "backmatter":
					return StructuralSemanticsProperty.Backmatter;
				case "volume":
					return StructuralSemanticsProperty.Volume;
				case "part":
					return StructuralSemanticsProperty.Part;
				case "chapter":
					return StructuralSemanticsProperty.Chapter;
				case "subchapter":
					return StructuralSemanticsProperty.Subchapter;
				case "division":
					return StructuralSemanticsProperty.Division;
				case "abstract":
					return StructuralSemanticsProperty.Abstract;
				case "foreword":
					return StructuralSemanticsProperty.Foreword;
				case "preface":
					return StructuralSemanticsProperty.Preface;
				case "prologue":
					return StructuralSemanticsProperty.Prologue;
				case "introduction":
					return StructuralSemanticsProperty.Introduction;
				case "preamble":
					return StructuralSemanticsProperty.Preamble;
				case "conclusion":
					return StructuralSemanticsProperty.Conclusion;
				case "epilogue":
					return StructuralSemanticsProperty.Epilogue;
				case "afterword":
					return StructuralSemanticsProperty.Afterword;
				case "epigraph":
					return StructuralSemanticsProperty.Epigraph;
				case "toc":
					return StructuralSemanticsProperty.Toc;
				case "toc-brief":
					return StructuralSemanticsProperty.TocBrief;
				case "landmarks":
					return StructuralSemanticsProperty.Landmarks;
				case "loa":
					return StructuralSemanticsProperty.Loa;
				case "loi":
					return StructuralSemanticsProperty.Loi;
				case "lot":
					return StructuralSemanticsProperty.Lot;
				case "lov":
					return StructuralSemanticsProperty.Lov;
				case "appendix":
					return StructuralSemanticsProperty.Appendix;
				case "colophon":
					return StructuralSemanticsProperty.Colophon;
				case "credits":
					return StructuralSemanticsProperty.Credits;
				case "keywords":
					return StructuralSemanticsProperty.Keywords;
				case "index":
					return StructuralSemanticsProperty.Index;
				case "index-headnotes":
					return StructuralSemanticsProperty.IndexHeadnotes;
				case "index-legend":
					return StructuralSemanticsProperty.IndexLegend;
				case "index-group":
					return StructuralSemanticsProperty.IndexGroup;
				case "index-entry-list":
					return StructuralSemanticsProperty.IndexEntryList;
				case "index-entry":
					return StructuralSemanticsProperty.IndexEntry;
				case "index-term":
					return StructuralSemanticsProperty.IndexTerm;
				case "index-editor-note":
					return StructuralSemanticsProperty.IndexEditorNote;
				case "index-locator":
					return StructuralSemanticsProperty.IndexLocator;
				case "index-locator-list":
					return StructuralSemanticsProperty.IndexLocatorList;
				case "index-locator-range":
					return StructuralSemanticsProperty.IndexLocatorRange;
				case "index-xref-preferred":
					return StructuralSemanticsProperty.IndexXrefPreferred;
				case "index-xref-related":
					return StructuralSemanticsProperty.IndexXrefRelated;
				case "index-term-category":
					return StructuralSemanticsProperty.IndexTermCategory;
				case "index-term-categories":
					return StructuralSemanticsProperty.IndexTermCategories;
				case "glossary":
					return StructuralSemanticsProperty.Glossary;
				case "glossterm":
					return StructuralSemanticsProperty.Glossterm;
				case "glossdef":
					return StructuralSemanticsProperty.Glossdef;
				case "bibliography":
					return StructuralSemanticsProperty.Bibliography;
				case "biblioentry":
					return StructuralSemanticsProperty.Biblioentry;
				case "titlepage":
					return StructuralSemanticsProperty.Titlepage;
				case "halftitlepage":
					return StructuralSemanticsProperty.Halftitlepage;
				case "copyright-page":
					return StructuralSemanticsProperty.CopyrightPage;
				case "seriespage":
					return StructuralSemanticsProperty.Seriespage;
				case "acknowledgments":
					return StructuralSemanticsProperty.Acknowledgments;
				case "imprint":
					return StructuralSemanticsProperty.Imprint;
				case "imprimatur":
					return StructuralSemanticsProperty.Imprimatur;
				case "contributors":
					return StructuralSemanticsProperty.Contributors;
				case "other-credits":
					return StructuralSemanticsProperty.OtherCredits;
				case "errata":
					return StructuralSemanticsProperty.Errata;
				case "dedication":
					return StructuralSemanticsProperty.Dedication;
				case "revision-history":
					return StructuralSemanticsProperty.RevisionHistory;
				case "case-study":
					return StructuralSemanticsProperty.CaseStudy;
				case "help":
					return StructuralSemanticsProperty.Help;
				case "marginalia":
					return StructuralSemanticsProperty.Marginalia;
				case "notice":
					return StructuralSemanticsProperty.Notice;
				case "pullquote":
					return StructuralSemanticsProperty.Pullquote;
				case "sidebar":
					return StructuralSemanticsProperty.Sidebar;
				case "tip":
					return StructuralSemanticsProperty.Tip;
				case "warning":
					return StructuralSemanticsProperty.Warning;
				case "halftitle":
					return StructuralSemanticsProperty.Halftitle;
				case "fulltitle":
					return StructuralSemanticsProperty.Fulltitle;
				case "covertitle":
					return StructuralSemanticsProperty.Covertitle;
				case "title":
					return StructuralSemanticsProperty.Title;
				case "subtitle":
					return StructuralSemanticsProperty.Subtitle;
				case "label":
					return StructuralSemanticsProperty.Label;
				case "ordinal":
					return StructuralSemanticsProperty.Ordinal;
				case "bridgehead":
					return StructuralSemanticsProperty.Bridgehead;
				case "learning-objective":
					return StructuralSemanticsProperty.LearningObjective;
				case "learning-objectives":
					return StructuralSemanticsProperty.LearningObjectives;
				case "learning-outcome":
					return StructuralSemanticsProperty.LearningOutcome;
				case "learning-outcomes":
					return StructuralSemanticsProperty.LearningOutcomes;
				case "learning-resource":
					return StructuralSemanticsProperty.LearningResource;
				case "learning-resources":
					return StructuralSemanticsProperty.LearningResources;
				case "learning-standard":
					return StructuralSemanticsProperty.LearningStandard;
				case "learning-standards":
					return StructuralSemanticsProperty.LearningStandards;
				case "answer":
					return StructuralSemanticsProperty.Answer;
				case "answers":
					return StructuralSemanticsProperty.Answers;
				case "assessment":
					return StructuralSemanticsProperty.Assessment;
				case "assessments":
					return StructuralSemanticsProperty.Assessments;
				case "feedback":
					return StructuralSemanticsProperty.Feedback;
				case "fill-in-the-blank-problem":
					return StructuralSemanticsProperty.FillInTheBlankProblem;
				case "general-problem":
					return StructuralSemanticsProperty.GeneralProblem;
				case "qna":
					return StructuralSemanticsProperty.Qna;
				case "match-problem":
					return StructuralSemanticsProperty.MatchProblem;
				case "multiple-choice-problem":
					return StructuralSemanticsProperty.MultipleChoiceProblem;
				case "practice":
					return StructuralSemanticsProperty.Practice;
				case "question":
					return StructuralSemanticsProperty.Question;
				case "practices":
					return StructuralSemanticsProperty.Practices;
				case "true-false-problem":
					return StructuralSemanticsProperty.TrueFalseProblem;
				case "panel":
					return StructuralSemanticsProperty.Panel;
				case "panel-group":
					return StructuralSemanticsProperty.PanelGroup;
				case "balloon":
					return StructuralSemanticsProperty.Balloon;
				case "text-area":
					return StructuralSemanticsProperty.TextArea;
				case "sound-area":
					return StructuralSemanticsProperty.SoundArea;
				case "annotation":
					return StructuralSemanticsProperty.Annotation;
				case "note":
					return StructuralSemanticsProperty.Note;
				case "footnote":
					return StructuralSemanticsProperty.Footnote;
				case "endnote":
					return StructuralSemanticsProperty.Endnote;
				case "rearnote":
					return StructuralSemanticsProperty.Rearnote;
				case "footnotes":
					return StructuralSemanticsProperty.Footnotes;
				case "endnotes":
					return StructuralSemanticsProperty.Endnotes;
				case "rearnotes":
					return StructuralSemanticsProperty.Rearnotes;
				case "annoref":
					return StructuralSemanticsProperty.Annoref;
				case "biblioref":
					return StructuralSemanticsProperty.Biblioref;
				case "glossref":
					return StructuralSemanticsProperty.Glossref;
				case "noteref":
					return StructuralSemanticsProperty.Noteref;
				case "backlink":
					return StructuralSemanticsProperty.Backlink;
				case "credit":
					return StructuralSemanticsProperty.Credit;
				case "keyword":
					return StructuralSemanticsProperty.Keyword;
				case "topic-sentence":
					return StructuralSemanticsProperty.TopicSentence;
				case "concluding-sentence":
					return StructuralSemanticsProperty.ConcludingSentence;
				case "pagebreak":
					return StructuralSemanticsProperty.Pagebreak;
				case "page-list":
					return StructuralSemanticsProperty.PageList;
				case "table":
					return StructuralSemanticsProperty.Table;
				case "table-row":
					return StructuralSemanticsProperty.TableRow;
				case "table-cell":
					return StructuralSemanticsProperty.TableCell;
				case "list":
					return StructuralSemanticsProperty.List;
				case "list-item":
					return StructuralSemanticsProperty.ListItem;
				case "figure":
					return StructuralSemanticsProperty.Figure;
				default:
					return StructuralSemanticsProperty.Unknown;
			}
		}
	}
}
