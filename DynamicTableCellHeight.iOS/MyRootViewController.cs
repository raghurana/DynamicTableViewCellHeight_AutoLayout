using System;
using System.Collections.Generic;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using UIKit;

namespace DynamicTableCellHeight.iOS
{
    public class MyRootViewController : UIViewController
    {
        private readonly UITableView tableView;

        public MyRootViewController()
        {
            tableView = new UITableView();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            var data = new List<Data>
            {
                new Data { Headline = "Rendezvous Hotel Melbourne", Body = "328 Flinders St, Melbourne VIC 3000" },
                new Data { Headline = "Adina Apartment Hotel Melbourne", Body = "189 Queen St, Melbourne VIC 3000. 189 Queen St, Melbourne VIC 3000.\r\n189 Queen St, Melbourne VIC 3000." },
                new Data { Headline = "Sydney Harbour Marriott Hotel at Circular Quay", Body = "30 Pitt St, Sydney NSW 2000" }
            };

            tableView.RegisterClassForCellReuse(typeof(MyCell), MyCell.CellId);
            tableView.Source = new MyTableSource(data);
            tableView.EstimatedRowHeight = 2f;
            tableView.RowHeight = UITableView.AutomaticDimension;

            View.Add(tableView);
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.AddConstraints
                (
                    tableView.WithSameTop(View).Plus(20),
                    tableView.WithSameLeft(View),
                    tableView.WithSameRight(View),
                    tableView.WithSameBottom(View)
                );
        }
    }

    public class MyTableSource : UITableViewSource
    {
        private readonly List<Data> dataLines;

        public MyTableSource(List<Data> dataLines)
        {
            this.dataLines = dataLines;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return dataLines.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (MyCell) tableView.DequeueReusableCell(MyCell.CellId, indexPath);

            cell.HeaderlineLabel.Text   = dataLines[indexPath.Row].Headline;
            cell.BodyLabel.Text         = dataLines[indexPath.Row].Body;

            return cell;
        }
    }

    public sealed class MyCell : UITableViewCell
    {
        private const float Margin = 10;

        public const string CellId = "MyCell";

        public UILabel HeaderlineLabel { get; }

        public UILabel BodyLabel { get; }

        public MyCell(IntPtr ptr)
            : base(ptr)
        {
            HeaderlineLabel = new UILabel { Lines = 0, Font = UIFont.PreferredHeadline };
            Add(HeaderlineLabel);

            BodyLabel = new UILabel { Lines = 0, Font = UIFont.PreferredBody };
            Add(BodyLabel);

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            this.AddConstraints
                (
                    HeaderlineLabel.AtTopOf(this, Margin),
                    HeaderlineLabel.WithSameLeft(this).Plus(Margin),
                    HeaderlineLabel.WithSameRight(this).Minus(Margin),

                    BodyLabel.Below(HeaderlineLabel).Plus(Margin/2),
                    BodyLabel.WithSameLeft(HeaderlineLabel),
                    BodyLabel.WithSameRight(HeaderlineLabel), 
                    BodyLabel.AtBottomOf(this, Margin)
                );
        }
    }       

    public class Data
    {
        public string Headline;

        public string Body;
    }
}