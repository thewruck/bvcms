using System; 
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;

namespace CmsData
{
	[Table(Name="dbo.BackgroundChecks")]
	public partial class BackgroundCheck : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
	#region Private Fields
		
		private int _Id;
		
		private DateTime _Created;
		
		private DateTime _Updated;
		
		private int _UserID;
		
		private int _StatusID;
		
		private int _PeopleID;
		
		private string _ServiceCode;
		
		private int _ReportID;
		
		private string _ReportLink;
		
		private int _IssueCount;
		
		private string _RequestXML;
		
		private string _ResponseXML;
		
		private string _ReportXML;
		
		private string _ErrorMessages;
		
   		
    	
		private EntityRef< Person> _Person;
		
		private EntityRef< Person> _User;
		
	#endregion
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
		
		partial void OnIdChanging(int value);
		partial void OnIdChanged();
		
		partial void OnCreatedChanging(DateTime value);
		partial void OnCreatedChanged();
		
		partial void OnUpdatedChanging(DateTime value);
		partial void OnUpdatedChanged();
		
		partial void OnUserIDChanging(int value);
		partial void OnUserIDChanged();
		
		partial void OnStatusIDChanging(int value);
		partial void OnStatusIDChanged();
		
		partial void OnPeopleIDChanging(int value);
		partial void OnPeopleIDChanged();
		
		partial void OnServiceCodeChanging(string value);
		partial void OnServiceCodeChanged();
		
		partial void OnReportIDChanging(int value);
		partial void OnReportIDChanged();
		
		partial void OnReportLinkChanging(string value);
		partial void OnReportLinkChanged();
		
		partial void OnIssueCountChanging(int value);
		partial void OnIssueCountChanged();
		
		partial void OnRequestXMLChanging(string value);
		partial void OnRequestXMLChanged();
		
		partial void OnResponseXMLChanging(string value);
		partial void OnResponseXMLChanged();
		
		partial void OnReportXMLChanging(string value);
		partial void OnReportXMLChanged();
		
		partial void OnErrorMessagesChanging(string value);
		partial void OnErrorMessagesChanged();
		
    #endregion
		public BackgroundCheck()
		{
			
			
			this._Person = default(EntityRef< Person>); 
			
			this._User = default(EntityRef< Person>); 
			
			OnCreated();
		}

		
    #region Columns
		
		[Column(Name="ID", UpdateCheck=UpdateCheck.Never, Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get { return this._Id; }

			set
			{
				if (this._Id != value)
				{
				
                    this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}

			}

		}

		
		[Column(Name="Created", UpdateCheck=UpdateCheck.Never, Storage="_Created", DbType="datetime NOT NULL")]
		public DateTime Created
		{
			get { return this._Created; }

			set
			{
				if (this._Created != value)
				{
				
                    this.OnCreatedChanging(value);
					this.SendPropertyChanging();
					this._Created = value;
					this.SendPropertyChanged("Created");
					this.OnCreatedChanged();
				}

			}

		}

		
		[Column(Name="Updated", UpdateCheck=UpdateCheck.Never, Storage="_Updated", DbType="datetime NOT NULL")]
		public DateTime Updated
		{
			get { return this._Updated; }

			set
			{
				if (this._Updated != value)
				{
				
                    this.OnUpdatedChanging(value);
					this.SendPropertyChanging();
					this._Updated = value;
					this.SendPropertyChanged("Updated");
					this.OnUpdatedChanged();
				}

			}

		}

		
		[Column(Name="UserID", UpdateCheck=UpdateCheck.Never, Storage="_UserID", DbType="int NOT NULL")]
		public int UserID
		{
			get { return this._UserID; }

			set
			{
				if (this._UserID != value)
				{
				
					if (this._User.HasLoadedOrAssignedValue)
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				
                    this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}

			}

		}

		
		[Column(Name="StatusID", UpdateCheck=UpdateCheck.Never, Storage="_StatusID", DbType="int NOT NULL")]
		public int StatusID
		{
			get { return this._StatusID; }

			set
			{
				if (this._StatusID != value)
				{
				
                    this.OnStatusIDChanging(value);
					this.SendPropertyChanging();
					this._StatusID = value;
					this.SendPropertyChanged("StatusID");
					this.OnStatusIDChanged();
				}

			}

		}

		
		[Column(Name="PeopleID", UpdateCheck=UpdateCheck.Never, Storage="_PeopleID", DbType="int NOT NULL")]
		public int PeopleID
		{
			get { return this._PeopleID; }

			set
			{
				if (this._PeopleID != value)
				{
				
					if (this._Person.HasLoadedOrAssignedValue)
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				
                    this.OnPeopleIDChanging(value);
					this.SendPropertyChanging();
					this._PeopleID = value;
					this.SendPropertyChanged("PeopleID");
					this.OnPeopleIDChanged();
				}

			}

		}

		
		[Column(Name="ServiceCode", UpdateCheck=UpdateCheck.Never, Storage="_ServiceCode", DbType="varchar(25) NOT NULL")]
		public string ServiceCode
		{
			get { return this._ServiceCode; }

			set
			{
				if (this._ServiceCode != value)
				{
				
                    this.OnServiceCodeChanging(value);
					this.SendPropertyChanging();
					this._ServiceCode = value;
					this.SendPropertyChanged("ServiceCode");
					this.OnServiceCodeChanged();
				}

			}

		}

		
		[Column(Name="ReportID", UpdateCheck=UpdateCheck.Never, Storage="_ReportID", DbType="int NOT NULL")]
		public int ReportID
		{
			get { return this._ReportID; }

			set
			{
				if (this._ReportID != value)
				{
				
                    this.OnReportIDChanging(value);
					this.SendPropertyChanging();
					this._ReportID = value;
					this.SendPropertyChanged("ReportID");
					this.OnReportIDChanged();
				}

			}

		}

		
		[Column(Name="ReportLink", UpdateCheck=UpdateCheck.Never, Storage="_ReportLink", DbType="varchar(255)")]
		public string ReportLink
		{
			get { return this._ReportLink; }

			set
			{
				if (this._ReportLink != value)
				{
				
                    this.OnReportLinkChanging(value);
					this.SendPropertyChanging();
					this._ReportLink = value;
					this.SendPropertyChanged("ReportLink");
					this.OnReportLinkChanged();
				}

			}

		}

		
		[Column(Name="IssueCount", UpdateCheck=UpdateCheck.Never, Storage="_IssueCount", DbType="int NOT NULL")]
		public int IssueCount
		{
			get { return this._IssueCount; }

			set
			{
				if (this._IssueCount != value)
				{
				
                    this.OnIssueCountChanging(value);
					this.SendPropertyChanging();
					this._IssueCount = value;
					this.SendPropertyChanged("IssueCount");
					this.OnIssueCountChanged();
				}

			}

		}

		
		[Column(Name="RequestXML", UpdateCheck=UpdateCheck.Never, Storage="_RequestXML", DbType="varchar")]
		public string RequestXML
		{
			get { return this._RequestXML; }

			set
			{
				if (this._RequestXML != value)
				{
				
                    this.OnRequestXMLChanging(value);
					this.SendPropertyChanging();
					this._RequestXML = value;
					this.SendPropertyChanged("RequestXML");
					this.OnRequestXMLChanged();
				}

			}

		}

		
		[Column(Name="ResponseXML", UpdateCheck=UpdateCheck.Never, Storage="_ResponseXML", DbType="varchar")]
		public string ResponseXML
		{
			get { return this._ResponseXML; }

			set
			{
				if (this._ResponseXML != value)
				{
				
                    this.OnResponseXMLChanging(value);
					this.SendPropertyChanging();
					this._ResponseXML = value;
					this.SendPropertyChanged("ResponseXML");
					this.OnResponseXMLChanged();
				}

			}

		}

		
		[Column(Name="ReportXML", UpdateCheck=UpdateCheck.Never, Storage="_ReportXML", DbType="varchar")]
		public string ReportXML
		{
			get { return this._ReportXML; }

			set
			{
				if (this._ReportXML != value)
				{
				
                    this.OnReportXMLChanging(value);
					this.SendPropertyChanging();
					this._ReportXML = value;
					this.SendPropertyChanged("ReportXML");
					this.OnReportXMLChanged();
				}

			}

		}

		
		[Column(Name="ErrorMessages", UpdateCheck=UpdateCheck.Never, Storage="_ErrorMessages", DbType="varchar")]
		public string ErrorMessages
		{
			get { return this._ErrorMessages; }

			set
			{
				if (this._ErrorMessages != value)
				{
				
                    this.OnErrorMessagesChanging(value);
					this.SendPropertyChanging();
					this._ErrorMessages = value;
					this.SendPropertyChanged("ErrorMessages");
					this.OnErrorMessagesChanged();
				}

			}

		}

		
    #endregion
        
    #region Foreign Key Tables
   		
	#endregion
	
	#region Foreign Keys
    	
		[Association(Name="FK_BackgroundChecks_People", Storage="_Person", ThisKey="PeopleID", IsForeignKey=true)]
		public Person Person
		{
			get { return this._Person.Entity; }

			set
			{
				Person previousValue = this._Person.Entity;
				if (((previousValue != value) 
							|| (this._Person.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if (previousValue != null)
					{
						this._Person.Entity = null;
						previousValue.BackgroundChecks.Remove(this);
					}

					this._Person.Entity = value;
					if (value != null)
					{
						value.BackgroundChecks.Add(this);
						
						this._PeopleID = value.PeopleId;
						
					}

					else
					{
						
						this._PeopleID = default(int);
						
					}

					this.SendPropertyChanged("Person");
				}

			}

		}

		
		[Association(Name="People__User", Storage="_User", ThisKey="UserID", IsForeignKey=true)]
		public Person User
		{
			get { return this._User.Entity; }

			set
			{
				Person previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if (previousValue != null)
					{
						this._User.Entity = null;
						previousValue.People.Remove(this);
					}

					this._User.Entity = value;
					if (value != null)
					{
						value.People.Add(this);
						
						this._UserID = value.PeopleId;
						
					}

					else
					{
						
						this._UserID = default(int);
						
					}

					this.SendPropertyChanged("User");
				}

			}

		}

		
	#endregion
	
		public event PropertyChangingEventHandler PropertyChanging;
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
				this.PropertyChanging(this, emptyChangingEventArgs);
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

   		
	}

}
