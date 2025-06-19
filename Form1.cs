using System.Runtime.InteropServices;

namespace ProjectWinForm
{

    public partial class Form1 : Form
    {
        AdminUserWrapper adminUserWrapper;
        List<CustomerWrapper> CustomerWrappers = new List<CustomerWrapper>();
        enum Pages { LoginPage, BookingPage, AdminPage, AddMovie, RemoveMovie,ShowBooking,
            CustomerDetails
        }

        Pages pageOpened = Pages.LoginPage;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            var loginlocations = rightPanel.Width / 2 - 250;
            leftPanel.Size = new Size((1980 / 100) * 15, leftPanel.Height);
            rightPanel.Location = new Point(leftPanel.Width + 10, 0);
            rightPanel.Size = new Size((1980 / 100) * 85 + 10, rightPanel.Height);
            LoginPageComponents();

            BackButton.Click += (s, e) => BackButtonClick();
            ShowBookingPageButton.Click += (s, e) => ShowBookings();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            adminUserWrapper = new AdminUserWrapper("admin", "password123");

            var MoviesLines = File.ReadAllLines("Movies.txt");

            foreach (var line in MoviesLines)
            {
                var parts = line.Split(',');
                if (parts.Length == 5)
                {
                    int id = int.Parse(parts[0]);
                    string title = parts[1];
                    string time = parts[2];
                    double price = double.Parse(parts[3]);
                    string filePath = parts[4];
                    MoviesWrappers.Add(new MoviesWrapper(id, title, time, price, filePath));
                }
            }

            var CustomerLines = File.ReadAllLines("Bookings.txt");

            foreach(var line in CustomerLines)
            {
                var parts = line.Split(',');
                if (parts.Length == 6)
                {
                    string name = parts[0];
                    string bookingID = parts[1];
                    int movieID = int.Parse(parts[2]);
                    int ticketQuantity = int.Parse(parts[3]);
                    bool loverSeat = bool.Parse(parts[4]);

                    CustomerWrappers.Add(new CustomerWrapper(name,bookingID,movieID,ticketQuantity,loverSeat));
                }
            }
        } 

        private void ShowBookings()
        {
            pageOpened = Pages.ShowBooking;

            RemovePreviousPageComponents();

            Panel panel = new Panel
            {
                Size = new Size(rightPanel.Width - 20, (rightPanel.Height / 3) + (rightPanel.Height / 3)),
                Location = new Point(10, 10),
                BackColor = SystemColors.MenuBar,
                AutoScroll = true
            };

            rightPanel.Controls.Add(panel);
            panel.VerticalScroll.Enabled = true;
            panel.HorizontalScroll.Enabled = false; 
            var CustomerLines = File.ReadAllLines("Bookings.txt");
            int yPos = 50;
            Label label = new Label
            {
                Text = "Name, Booking ID, Movie ID, Ticket Quantity, LoverSeat,Movie Name",
                AutoSize = true,
                Location = new Point(10, 10),
                Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold)
            };
            foreach (var line in CustomerLines)
            {
                Label bookingLabel = new Label
                {
                    Text = line,
                    AutoSize = true,
                    Location = new Point(10, yPos),
                    Font = new Font(FontFamily.GenericSansSerif, 12)
                };
                yPos += 30;

                panel.Controls.Add(label);
                panel.Controls.Add(bookingLabel);
            }
        }

        #region LoginPage
        private Button LoginButton;
        private TextBox PasswordTextBox;
        private TextBox UserNameTextBox;
        private Label PasswordLabel;
        private Label UserNameLabel;

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (adminUserWrapper.CheckPassword(UserNameTextBox.Text, PasswordTextBox.Text))
            {
                MessageBox.Show("Login Successful!");
                RemovePreviousPageComponents();
                AdminPageComponents();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void LoginPageComponents()
        {
            RemovePreviousPageComponents();

            LoginButtonCreate();

            PasswordTextBoxCreate();

            UserNameTextBoxCreate();

            PasswordLabelCreate();

            UserNameLabelCreate();

            LoginButton.Click += LoginButton_Click;
            rightPanel.Controls.Add(LoginButton);
            rightPanel.Controls.Add(PasswordTextBox);
            rightPanel.Controls.Add(UserNameTextBox);
            rightPanel.Controls.Add(PasswordLabel);
            rightPanel.Controls.Add(UserNameLabel);

            LoginPageSizesAndLocations();

            pageOpened = Pages.LoginPage;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoginPageComponents();
        }

        private void LoginButtonCreate()
        {
            LoginButton = new Button
            {
                Name = "LoginButton",
                Text = "Login",
                TabIndex = 4,
                UseVisualStyleBackColor = true
            };
        }

        private void PasswordTextBoxCreate()
        {
            PasswordTextBox = new TextBox
            {
                Name = "PasswordTextBox",
                Font = new Font("Microsoft Sans Serif", 16F),
                TabIndex = 3,
                UseSystemPasswordChar = true
            };
        }

        private void UserNameTextBoxCreate()
        {
            UserNameTextBox = new TextBox
            {
                Name = "UserNameTextBox",
                Font = new Font("Microsoft Sans Serif", 16F),
                TabIndex = 2
            };
        }

        private void PasswordLabelCreate()
        {
            PasswordLabel = new Label
            {
                Name = "PasswordLabel",
                Text = "Password",
                Font = new Font("Microsoft Sans Serif", 16F),
                BackColor = Color.Transparent
            };
        }

        private void UserNameLabelCreate()
        {
            UserNameLabel = new Label
            {
                Text = "UserName",
                Font = new Font("Microsoft Sans Serif", 16F),
                Name = "UserNameLabel",
                BackColor = Color.Transparent
            };
        }

        private void LoginPageSizesAndLocations()
        {
            var loginlocations = rightPanel.Width / 2 - 250;

            UserNameLabel.Location = new Point(loginlocations, 250);
            UserNameLabel.Size = new Size(rightPanel.Width / 2, 40);
            UserNameLabel.TextAlign = ContentAlignment.MiddleLeft;

            UserNameTextBox.Location = new Point(loginlocations, UserNameLabel.Location.Y + 50);
            UserNameTextBox.Size = new Size(400, 44);

            PasswordLabel.Location = new Point(loginlocations, UserNameLabel.Location.Y + 100);
            PasswordLabel.Size = new Size(rightPanel.Width / 2, 40);
            PasswordLabel.TextAlign = ContentAlignment.MiddleLeft;

            PasswordTextBox.Location = new Point(loginlocations, PasswordLabel.Location.Y + 50);
            PasswordTextBox.Size = new Size(400, 44);

            LoginButton.Size = new Size(180, 35);
            LoginButton.Location = new Point(loginlocations + 110, PasswordTextBox.Location.Y + 70);
        }

        #endregion

        #region BookingPage

        private List<MoviesWrapper> MoviesWrappers = new List<MoviesWrapper>();
        private List<PictureBox> pictureBoxes = new List<PictureBox>();
        private List<Label> MovieTitles = new List<Label>();
        private Label MovieTitle = new Label();
        private Label MovieDetails = new Label();
        private Panel MoviesPanel;
        private Panel MovieDetailPanel;

        private void RemovePreviousPageComponents()
        {
            for (int i = rightPanel.Controls.Count - 1; i >= 0; i--)
            {
                var control = rightPanel.Controls[i];
                rightPanel.Controls.RemoveAt(i);
                control.Dispose();
            }
            pictureBoxes.Clear();
            MovieTitles.Clear();
        }

        private void BookingPageComponents()
        {
            CreateMoviesPanel();

            CreateMoviesBanner();

            PictureBoxesSubscribeToMovieDetails();

            pageOpened = Pages.BookingPage;
        }

        private void CreateMoviesPanel()
        {
            MoviesPanel = new Panel
            {
                Name = "MoviesPanel",
                Location = new Point(10, 10),
                Size = new Size(400, (rightPanel.Size.Height / 2) + (rightPanel.Size.Height / 3)),
                BackColor = SystemColors.GrayText,
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true,

            };
            MoviesPanel.VerticalScroll.Enabled = true;
            MoviesPanel.HorizontalScroll.Enabled = false;
            rightPanel.Controls.Add(MoviesPanel);

            MovieDetailPanel = new Panel
            {
                Name = "DetailsPanel",
                Location = new Point(MoviesPanel.Width + MoviesPanel.Location.X + 10, 10),
                Size = new Size(rightPanel.Width - MoviesPanel.Width - MoviesPanel.Location.X - 20, MoviesPanel.Height),
                BackColor = Color.DeepPink
            };
            rightPanel.Controls.Add(MovieDetailPanel);

        }

        private void CreateMoviesBanner()
        {
            for (int i = 0; i < MoviesWrappers.Count; i++)
            {
                var movie = MoviesWrappers[i];
                pictureBoxes.Add(
                        new PictureBox
                        {
                            Name = movie.Title + "pic",
                            Size = new Size(350, 350),
                            BackgroundImage = Image.FromFile(movie.FilePath),
                            BackgroundImageLayout = ImageLayout.Stretch,
                        }
                );
                if (i == 0) pictureBoxes[i].Location
                        = new Point(10, 20);
                else
                    pictureBoxes[i].Location
                            = new Point(10, pictureBoxes[i - 1].Location.Y + 450);

                MovieTitles.Add
                    (
                        new Label
                        {
                            Text = movie.Title,
                            AutoSize = false,
                            TextAlign = ContentAlignment.TopCenter,
                            Size = new Size(pictureBoxes[i].Width, 80),
                            Location = new Point(pictureBoxes[i].Location.X, pictureBoxes[i].Location.Y + 340),
                            Font = new Font(FontFamily.GenericSansSerif, 16, FontStyle.Bold),
                        }
                    );
                MoviesPanel.Controls.Add(MovieTitles.Last());
                MoviesPanel.Controls.Add(pictureBoxes.Last());
            }
        }

        private void PictureBoxesSubscribeToMovieDetails()
        {
            for (int i = 0; i < MoviesWrappers.Count; i++)
            {
                int index = i;
                pictureBoxes[i].Click += (s, e) => ShowMovieDetails(MoviesWrappers[index]);
            }
        }

        private void ShowMovieDetails(MoviesWrapper movie)
        {
            if(MovieTitle.IsDisposed){
                MovieTitle = new Label();
                MovieDetails = new Label();
            }
            MovieTitle.Text = movie.Title;
            MovieTitle.AutoSize = true;
            MovieTitle.TextAlign = ContentAlignment.TopLeft;
            MovieTitle.Location = new Point(10, 10);
            MovieTitle.Font = new Font(FontFamily.GenericSansSerif, 40, FontStyle.Bold);

            MovieDetails.Text = $"Id: " + movie.Id.ToString() + $"\nPrice: {movie.Price} Time: {movie.Time} ";
            MovieDetails.AutoSize = true;
            MovieDetails.TextAlign = ContentAlignment.TopLeft;
            MovieDetails.Location = new Point(10, 50);
            MovieDetails.Font = new Font(FontFamily.GenericSansSerif, 25);



            if (pageOpened == Pages.BookingPage)
            {
                Button BookButton = new Button
                {
                    Text = "Book Now",
                    Size = new Size(100, 30),
                    Location = new Point(MovieDetailPanel.Width - 120, MovieDetailPanel.Height - 50)
                };
                BookButton.Click += (s, e) => CustomerDetails(movie.Id);
                MovieDetailPanel.Controls.Add(BookButton);
            }
            MovieDetailPanel.Controls.Add(MovieTitle);
            MovieDetailPanel.Controls.Add(MovieDetails);
        }
        private void CustomerDetails(int movieID)
        {
            pageOpened = Pages.CustomerDetails;
            RemovePreviousPageComponents();
            Panel panel = new Panel
            {
                Size = new Size(rightPanel.Width - 20, (rightPanel.Height / 3) + (rightPanel.Height / 3)),
                Location = new Point(10, 10),
                BackColor = Color.Transparent
            };


            Label NameLabel = new Label
            {
                Text = "Name",
                Size = new Size(200, 30),
                Location = new Point(rightPanel.Width / 2 - 200, 50),
                Font = new Font(FontFamily.GenericSansSerif, 16)
            };

            Label TicketQuantityLabel = new Label
            {
                Text = "Ticket Quantity",
                Size = new Size(200, 30),
                Location = new Point(rightPanel.Width / 2 - 200, 150),
                Font = new Font(FontFamily.GenericSansSerif, 16)
            };
            Label LoverSeatLabel = new Label
            {
                Text = "Lover Seat",
                Size = new Size(200, 30),
                Location = new Point(rightPanel.Width / 2 - 200, 250),
                Font = new Font(FontFamily.GenericSansSerif, 16)
            };

            TextBox NameBox = new TextBox
            {
                Size = new Size(200, 30),
                Location = new Point(rightPanel.Width / 2 + 100, 50)
            };
            TextBox TicketQuantityBox = new TextBox
            {
                Size = new Size(200, 30),
                Location = new Point(rightPanel.Width / 2 + 100, 150)
            };
            CheckBox LoverSeatCheckBox = new CheckBox
            {
                Text = "Yes",
                Size = new Size(100, 30),
                Location = new Point(rightPanel.Width / 2 + 100, 250)
            };
            Button BookButton = new Button
            {
                Text = "Book",
                Size = new Size(100, 30),
                Location = new Point(rightPanel.Width / 2 - 100, 450)
            };
            
            panel.Controls.Add(NameLabel);
            panel.Controls.Add(TicketQuantityLabel);
            panel.Controls.Add(LoverSeatLabel);
            panel.Controls.Add(NameBox);
            panel.Controls.Add(TicketQuantityBox);
            panel.Controls.Add(LoverSeatCheckBox);
            panel.Controls.Add(BookButton);
            rightPanel.Controls.Add(panel);

            string BookingId;
            if(CustomerWrappers.Count == 0)
            {
                BookingId = "0";
            }
            else
            {
                BookingId = (int.Parse(CustomerWrappers.Last().BookingID) + 1).ToString();
            }

                BookButton.Click += (s, e) => SaveBookingToTxt(NameBox.Text,
                    BookingId,
                    movieID,
                    int.Parse(TicketQuantityBox.Text),
                    LoverSeatCheckBox.Checked);

        }
        private void SaveBookingToTxt(string name,string booking,int movieID,int ticketQuantity,bool loverSeat)
        {
            CustomerWrappers.Add(new CustomerWrapper(name, booking, movieID, ticketQuantity, loverSeat));
            using (StreamWriter writer = new StreamWriter("Bookings.txt"))
            {
                foreach (var customer in CustomerWrappers)
                {
                    string line = $"{customer.Name}   ,{customer.BookingID}  ,{customer.MovieID},   {customer.TicketQuantity},   {customer.LoverSeat},\t\t{MoviesWrappers.FirstOrDefault(m => m.Id == movieID)?.Title}";
                    writer.WriteLine(line);
                }
            }
            MessageBox.Show(name + " has booked " + MoviesWrappers.FirstOrDefault(m => m.Id == movieID)?.Title + " successfully!");
        }

        #endregion

        #region AdminPage

        private void AdminPageComponents() //Displays 2 buttons
        {
            pageOpened = Pages.AdminPage;

            int ButtonX = 200;
            int ButtonY = 70;

            Button AddMovieButton = new Button
            {
                Text = "AddMovie",
                Size = new Size(ButtonX,ButtonY),
                Location = new Point(rightPanel.Width / 2 - 100, rightPanel.Height / 2 - 100),
                BackColor = Color.LightCoral
            };
            AddMovieButton.Click += (e, s) => AddMovieButtonClick();

            Button RemoveMovieButton = new Button
            {
                Text = "Remove Movie",
                Size = new Size(ButtonX, ButtonY),
                Location = new Point(rightPanel.Width / 2 - 100, rightPanel.Height / 2),
                BackColor = Color.LightCoral
            };
            RemoveMovieButton.Click += (e, s) => RemoveMovieButtonClick();

            rightPanel.Controls.Add(AddMovieButton);
            rightPanel.Controls.Add(RemoveMovieButton);
        }

        private void AddMovieButtonClick()
        {
            RemovePreviousPageComponents();
            pageOpened = Pages.AddMovie;

            Panel panel = new Panel
            {
                Size = new Size(rightPanel.Width - 20, (rightPanel.Height / 3) + (rightPanel.Height / 3)),
                Location = new Point(10, 10),
                BackColor = Color.Transparent,
                BackgroundImage = Image.FromFile("C:\\Users\\Laptop Solutions\\Downloads\\blueG.png"),
            };
            rightPanel.Controls.Add(panel);

            int fontSize = 16;
            int labelX = 100;
            int labelY = 50;
            Label Idlabel = new Label
            {
                Text = "Id:",
                Size = new Size(labelX, labelY),
                Font = new Font(FontFamily.GenericSansSerif, fontSize),
                Location = new Point(rightPanel.Width / 2 - 200, 100)
            };
            Label Titlelabel = new Label
            {
                Text = "Title:",
                Size = new Size(labelX, labelY),
                Font = new Font(FontFamily.GenericSansSerif, fontSize),
                Location = new Point(rightPanel.Width / 2 - 200, 200)
            };
            Label Timelabel = new Label
            {
                Text = "Time:",
                Size = new Size(labelX, labelY),
                Font = new Font(FontFamily.GenericSansSerif, fontSize),
                Location = new Point(rightPanel.Width / 2 - 200, 300)
            };
            Label Pricelabel = new Label
            {
                Text = "Price:",
                Size = new Size(labelX, labelY),
                Font = new Font(FontFamily.GenericSansSerif, fontSize),
                Location = new Point(rightPanel.Width / 2 - 200, 400)
            };

            TextBox IdBox = new TextBox
            {
                Size = new Size(200, 30),
                Location = new Point(rightPanel.Width / 2 , 100)
            };
            TextBox TitleBox = new TextBox
            {
                Size = new Size(200, 30),
                Location = new Point(rightPanel.Width / 2 , 200)
            };
            TextBox TimeBox = new TextBox
            {
                Size = new Size(200, 30),
                Location = new Point(rightPanel.Width / 2 , 300)
            };
            TextBox PriceBox = new TextBox
            {
                Size = new Size(200, 30),
                Location = new Point(rightPanel.Width / 2 , 400)
            };

            Button AddButton = new Button
            {
                Text = "Add",
                Size = new Size(200, 50),
                Location = new Point(rightPanel.Width / 2  - 100, 500),
                BackColor = Color.Coral,
            };

            AddButton.Click += (s, e) => AddButtonClick(IdBox, TitleBox, TimeBox, PriceBox);


            panel.Controls.Add(AddButton);
            panel.Controls.Add(IdBox);
            panel.Controls.Add(PriceBox);
            panel.Controls.Add(TitleBox);
            panel.Controls.Add(TimeBox);

            panel.Controls.Add(Idlabel);
            panel.Controls.Add(Titlelabel);
            panel.Controls.Add(Pricelabel);
            panel.Controls.Add(Timelabel);

        }

        private void RemoveMovieButtonClick()
        {
            RemovePreviousPageComponents();

            pageOpened = Pages.RemoveMovie;

            SaveMoviesToTxt(MoviesWrappers);

            CreateMoviesPanel();

            CreateMoviesBanner();

            PictureBoxesSubscribeToMovieDetails();

            SubscribeRemoveMovieButtonCreate();

        }

        private void SubscribeRemoveMovieButtonCreate()
        {
            for (int i = 0; i < MoviesWrappers.Count; i++)
            {
                int index = i;
                pictureBoxes[i].Click += (s, e) => CreateFinalMovieRemoveButton(MoviesWrappers[index]);
            }
        }
        Button RemoveMovie;

        private void CreateFinalMovieRemoveButton(MoviesWrapper movie)
        {
            RemoveMovie = new Button
            {
                Text = "Remove Movie",
                Size = new Size(200, 50),
                Location = new Point(MovieDetailPanel.Width - 220, MovieDetailPanel.Height - 60),
            };

            RemoveMovie.Click += (s, e) =>
            {
                MoviesWrappers.Remove(movie);
                RemoveMovieButtonClick();
            };

            MovieDetailPanel.Controls.Add(RemoveMovie);
        }

        private void AddButtonClick(TextBox IdBox, TextBox TitleBox, TextBox TimeBox, TextBox PriceBox)
        {
            string FilePath;
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Select an Image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FilePath = ofd.FileName;
                MoviesWrappers.Add
                (new MoviesWrapper
                (int.Parse(IdBox.Text),
                TitleBox.Text,
                TimeBox.Text,
                Double.Parse(PriceBox.Text),
                FilePath));
                string sourcePath = ofd.FileName;

                // Path to the release folder (bin\Release\netX\)
                string releasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

                // Create the folder if it doesn't exist
                if (!Directory.Exists(releasePath))
                    Directory.CreateDirectory(releasePath);

                string fileName = Path.GetFileName(sourcePath);
                string destPath = Path.Combine(releasePath, fileName);

                    File.Copy(sourcePath, destPath, true); // Overwrite if exists
            }

            MessageBox.Show("Movie Added Successfully!");
            SaveMoviesToTxt(MoviesWrappers);
            BackButtonClick();
        }

        private void BackButtonClick()
        {
            if (pageOpened == Pages.AddMovie || pageOpened == Pages.RemoveMovie)
            {
                RemovePreviousPageComponents();
                AdminPageComponents();
            }
            else if (pageOpened == Pages.BookingPage || pageOpened == Pages.AdminPage)
            {
                RemovePreviousPageComponents();
                LoginPageComponents();
            }else if(pageOpened == Pages.CustomerDetails)
            {
                RemoveMovieButtonClick();
                BookingPageComponents();
            }
        }

        #endregion

        private void BookingPage_Click(object sender, EventArgs e)
        {
            RemovePreviousPageComponents();

            BookingPageComponents();
        }

        public void SaveMoviesToTxt(List<MoviesWrapper> movies)
        {
            using (StreamWriter writer = new StreamWriter("Movies.txt"))
            {
                foreach (var movie in movies)
                {
                    string line = $"{movie.Id},{movie.Title},{movie.Time},{movie.Price},{movie.FilePath}";
                    writer.WriteLine(line);
                }
            }
        }

        public class MoviesWrapper : IDisposable
        {
            // Native method imports
            [DllImport("Movies.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            private static extern IntPtr CreateMovieClass(int id, string title, string time, double price, string filePathOrURL);

            [DllImport("Movies.dll", CallingConvention = CallingConvention.Cdecl)]
            private static extern void DestroyMovieClass(IntPtr instance);

            [DllImport("Movies.dll", CallingConvention = CallingConvention.Cdecl)]
            private static extern double DisplayPrice(IntPtr instance);

            [DllImport("Movies.dll", CallingConvention = CallingConvention.Cdecl)]
            private static extern int DisplayId(IntPtr instance);

            [DllImport("Movies.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            private static extern IntPtr DisplayTitle(IntPtr instance);

            [DllImport("Movies.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            private static extern IntPtr DisplayTime(IntPtr instance);

            [DllImport("Movies.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            private static extern IntPtr DisplayFilePath(IntPtr instance);

            private IntPtr _nativeInstance;
            private bool _disposed = false;

            public MoviesWrapper(int id, string title, string time, double price, string filePath)
            {
                _nativeInstance = CreateMovieClass(id, title, time, price, filePath);
                if (_nativeInstance == IntPtr.Zero)
                {
                    throw new Exception("Failed to create Movies instance");
                }
            }

            public double Price => DisplayPrice(_nativeInstance);
            public int Id => DisplayId(_nativeInstance);

            public string Title
            {
                get
                {
                    IntPtr ptr = DisplayTitle(_nativeInstance);
                    return Marshal.PtrToStringAnsi(ptr);
                }
            }
            public string Time
            {
                get
                {
                    IntPtr ptr = DisplayTime(_nativeInstance);
                    return Marshal.PtrToStringAnsi(ptr);
                }
            }

            public string FilePath
            {
                get
                {
                    IntPtr ptr = DisplayFilePath(_nativeInstance);
                    string result = Marshal.PtrToStringAnsi(ptr);
                    return result;
                }
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (_nativeInstance != IntPtr.Zero)
                    {
                        DestroyMovieClass(_nativeInstance);
                        _nativeInstance = IntPtr.Zero;
                    }
                    _disposed = true;
                }
            }
            ~MoviesWrapper()
            {
                Dispose(false);
            }
        }

        public class AdminUserWrapper : IDisposable
        {
            // Native method imports
            [DllImport("Admin.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            private static extern IntPtr CreateAdminUserClass(string username, string password);

            [DllImport("Admin.dll", CallingConvention = CallingConvention.Cdecl)]
            private static extern void DestroyAdminUserClass(IntPtr instance);

            [DllImport("Admin.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            [return: MarshalAs(UnmanagedType.I1)]
            private static extern bool AdminUserClass_PasswordCheck(IntPtr instance, string username, string password);

            private IntPtr _nativeInstance;
            private bool _disposed = false;

            public AdminUserWrapper(string username, string password)
            {
                _nativeInstance = CreateAdminUserClass(username, password);
                if (_nativeInstance == IntPtr.Zero)
                {
                    throw new Exception("Failed to create AdminUser instance");
                }
            }

            public bool CheckPassword(string username, string password)
            {
                if (_disposed) throw new ObjectDisposedException("AdminUserWrapper");
                return AdminUserClass_PasswordCheck(_nativeInstance, username, password);
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (_nativeInstance != IntPtr.Zero)
                    {
                        DestroyAdminUserClass(_nativeInstance);
                        _nativeInstance = IntPtr.Zero;
                    }
                    _disposed = true;
                }
            }

            ~AdminUserWrapper()
            {
                Dispose(false);
            }
        }

        public class CustomerWrapper
        {
            [DllImport("Customer.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            private static extern IntPtr CreateCustomerClass(string n,string b,int movieID,int ticketQuantity,bool loverseat);

            [DllImport("Customer.dll", CallingConvention = CallingConvention.Cdecl)]
            private static extern void DestroyCustomerClass(IntPtr instance);

            [DllImport("Customer.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            private static extern IntPtr GetCustomerName(IntPtr instance);

            [DllImport("Customer.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
            private static extern IntPtr GetCustomerBookingID(IntPtr instance);

            [DllImport("Customer.dll", CallingConvention = CallingConvention.Cdecl)]
            private static extern int GetCustomerMovieID(IntPtr instance);

            [DllImport("Customer.dll", CallingConvention = CallingConvention.Cdecl)]
            private static extern int GetCustomerTicketQuantity(IntPtr instance);

            [DllImport("Customer.dll", CallingConvention = CallingConvention.Cdecl)]
            private static extern bool GetCustomerLoverSeat(IntPtr instance);

            private IntPtr _nativeInstance;
            private bool _disposed = false;

            public CustomerWrapper(string name, string bookingID, int movieID, int ticketQuantity, bool loverSeat)
            {
                _nativeInstance = CreateCustomerClass(name, bookingID, movieID, ticketQuantity, loverSeat);
                if (_nativeInstance == IntPtr.Zero)
                {
                    throw new Exception("Failed to create Customer instance");
                }
            }

            public string Name
            {
                get
                {
                    IntPtr ptr = GetCustomerName(_nativeInstance);
                    return Marshal.PtrToStringAnsi(ptr);
                }
            }
            public string BookingID
            {
                get
                {
                    IntPtr ptr = GetCustomerBookingID(_nativeInstance);
                    return Marshal.PtrToStringAnsi(ptr);
                }
            }
            public int MovieID
            {
                get
                {
                    return GetCustomerMovieID(_nativeInstance);
                }
            }
            public int TicketQuantity
            {
                get
                {
                    return GetCustomerTicketQuantity(_nativeInstance);
                }
            }
            public bool LoverSeat
            {
                get
                {
                    return GetCustomerLoverSeat(_nativeInstance);
                }
            }
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (_nativeInstance != IntPtr.Zero)
                    {
                        DestroyCustomerClass(_nativeInstance);
                        _nativeInstance = IntPtr.Zero;
                    }
                    _disposed = true;
                }
            }
            ~CustomerWrapper()
            {
                Dispose(false);
            }
        }
    }
}
