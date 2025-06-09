using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using WinFormAnimation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace ProjectWinForm
{

    public partial class Form1 : Form
    {
        AdminUserWrapper adminUserWrapper;
        enum Pages { LoginPage, BookingPage, AdminPage }

        Pages pageOpened = Pages.LoginPage;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            var loginlocations = rightPanel.Width / 2 - 250;
            leftPanel.Size = new Size((1980 / 100) * 15, leftPanel.Height);
            rightPanel.Location = new Point(leftPanel.Width, 0);
            rightPanel.Size = new Size((1980 / 100) * 85 + 20, rightPanel.Height);
            LoginPageComponents();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            adminUserWrapper = new AdminUserWrapper("admin", "password123");
            MoviesWrappers.Add(new MoviesWrapper(1, "FIFTY SHADES OF GREY", "12:00 AM", 2000.00, "C:/Users/Laptop Solutions/Downloads/GenAsim.jpeg"));
            MoviesWrappers.Add(new MoviesWrapper(1, "FIFTY SHADES OF GREY", "12:00 AM", 2000.00, "C:/Users/Laptop Solutions/Downloads/GenAsim.jpeg"));
            MoviesWrappers.Add(new MoviesWrapper(1, "FIFTY SHADES OF GREY", "12:00 AM", 2000.00, "C:/Users/Laptop Solutions/Downloads/GenAsim.jpeg"));
            MoviesWrappers.Add(new MoviesWrapper(1, "FIFTY SHADES OF GREY", "12:00 AM", 2000.00, "C:/Users/Laptop Solutions/Downloads/GenAsim.jpeg"));
            MoviesWrappers.Add(new MoviesWrapper(1, "FIFTY SHADES OF GREY", "12:00 AM", 2000.00, "C:/Users/Laptop Solutions/Downloads/GenAsim.jpeg"));
        }

        #region LoginPage
        private Button LoginButton;
        private Button CustomerButton;
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
                CustomerButton = new Button
                {
                    Text = "Exit",
                    Location = new Point(rightPanel.Width / 2 - 50, PasswordTextBox.Location.Y + 100),
                    Size = new Size(100, 30)
                };
                this.Controls.Add(CustomerButton);
                //check Admin 
                //Add Moviess
                //Delete Moviess
                //Available Moviess
            }
        }
        private void LoginPageComponents()
        {
            RemovePreviousPageComponents();

            LoginButtonCreate();

            CustomerButtonCreate();

            PasswordTextBoxCreate();

            UserNameTextBoxCreate();

            PasswordLabelCreate();

            UserNameLabelCreate();

            LoginButton.Click += LoginButton_Click;
            rightPanel.Controls.Add(CustomerButton);
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
        private void CustomerButtonCreate()
        {
            CustomerButton = new Button
            {
                Name = "CustomerButton",
                Text = "CustomerPortal",
                TabIndex = 5,
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
                TabIndex = 1,
            };
        }
        private void UserNameLabelCreate()
        {
            UserNameLabel = new Label
            {
                Text = "UserName",
                Font = new Font("Microsoft Sans Serif", 16F),
                Name = "UserNameLabel",
                TabIndex = 0,
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

            CustomerButton.Size = new Size(180, 35);
            CustomerButton.Location = new Point(loginlocations + 110, PasswordTextBox.Location.Y + 250);

            LoginButton.Size = new Size(180, 35);
            LoginButton.Location = new Point(loginlocations + 110, PasswordTextBox.Location.Y + 70);
        }
        #endregion

        #region BookingPage

        private List<MoviesWrapper> MoviesWrappers = new List<MoviesWrapper>();
        private List<PictureBox> pictureBoxes = new List<PictureBox>();
        private List<Label> MovieTitles = new List<Label>();
        private Label MovieTitle;
        private Label MovieDetails;
        private Panel MoviesPanel;
        private VScrollBar scrollBar;
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
                BackColor = SystemColors.MenuHighlight
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
                pictureBoxes.Last().Click += (s, e) => ShowMovieDetails(movie);

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
        private void ShowMovieDetails(MoviesWrapper movie)
        {
            MovieTitle = new Label
            {
                Text = movie.Title,
                AutoSize = true,
                TextAlign = ContentAlignment.TopLeft,
                Location = new Point(10, 10),
                Font = new Font(FontFamily.GenericSansSerif, 40, FontStyle.Bold)
            };
            MovieDetails = new Label
            {
                Text = $"Id: " + movie.Id.ToString() + $"\nPrice: {movie.Price} Time: {movie.Time} ",
                AutoSize = true,
                TextAlign = ContentAlignment.TopLeft,
                Location = new Point(10, 50),
                Font = new Font(FontFamily.GenericSansSerif, 25)
            };
            MovieDetailPanel.Controls.Add(MovieTitle);
            MovieDetailPanel.Controls.Add(MovieDetails);
        }

        #endregion

        #region AdminPage

        void AdminPageComponents()
        {
            Button AddMovieButton = new Button
            {
                Text = "AddMovie",
                Size = new Size(100, 30),
                Location = new Point(rightPanel.Width / 2, rightPanel.Height / 2),
            };
            AddMovieButton.Click += (e, s) => AddMovieButtonClick(AddMovieButton);
            Button RemoveMovieButton = new Button();
            rightPanel.Controls.Add(AddMovieButton);
        }

        void AddMovieButtonClick(Button button)
        {
            button.Dispose();
            MessageBox.Show("Worked");
            Panel panel = new Panel
            {
                Size = new Size(rightPanel.Width - 70, (rightPanel.Height / 3) + (rightPanel.Height / 3)),
                Location = new Point(10, 10),
                BackColor = SystemColors.MenuBar
            };
            rightPanel.Controls.Add(panel);

            Label Idlabel = new Label
            {
                Text = "Id",
                Size = new Size(100, 30),
                Location = new Point(rightPanel.Width / 2 - 100, 500)
            };
            Label Titlelabel = new Label
            {
                Text = "Title",
                Size = new Size(100, 30),
                Location = new Point(rightPanel.Width / 2 - 100, 500)
            }; 
            Label Timelabel = new Label
            {
                Text = "Time",
                Size = new Size(100, 30),
                Location = new Point(rightPanel.Width / 2 - 100, 500)
            }; 
            Label Pricelabel = new Label
            {
                Text = "Price",
                Size = new Size(100, 30),
                Location = new Point(rightPanel.Width / 2 - 100, 500)
            };
            Label Imagelabel = new Label
            {
                Text = "Image",
                Size = new Size(100, 30),
                Location = new Point(rightPanel.Width / 2 - 100, 500)
            };

            TextBox IdBox = new TextBox
            {
                Size = new Size(100, 30),
                Location = new Point(rightPanel.Width / 2 + 100, 100)
            };
            TextBox TitleBox = new TextBox
            {
                Size = new Size(100, 30),
                Location = new Point(rightPanel.Width / 2 + 100, 200)
            };
            TextBox TimeBox = new TextBox
            {
                Size = new Size(100, 30),
                Location = new Point(rightPanel.Width / 2 + 100, 300)
            };
            TextBox PriceBox = new TextBox
            {
                Size = new Size(100, 30),
                Location = new Point(rightPanel.Width / 2 + 100, 400)
            };
            Button ImageButton = new Button
            {
                Text = "Image File",
                Size = new Size(100, 30),
                Location = new Point(rightPanel.Width / 2 - 100, 500)
            };

            Button BackButton = new Button
            {
                Text = "Back",
                Size = new Size(100, 30),
                Location = new Point(10, 10)
            };
            Button AddButton = new Button
            {
                Text = "Add",
                Size = new Size(100, 30),
                Location = new Point(rightPanel.Width / 2 + 100, 600)
            };

            string FilePath = GetImage();

            ImageButton.Click += (s, e) => GetImage(FilePath);
            BackButton.Click += (s, e) => BackButtonClick();
            AddButton.Click += (s, e) => AddButtonClick(IdBox,TitleBox,TimeBox,PriceBox,FilePath);


            panel.Controls.Add(AddButton);
            panel.Controls.Add(BackButton);
            panel.Controls.Add(IdBox);
            panel.Controls.Add(PriceBox);
            panel.Controls.Add(ImageButton);
            panel.Controls.Add(TitleBox);
            panel.Controls.Add(TimeBox);

            panel.Controls.Add(Idlabel);
            panel.Controls.Add(Titlelabel);
            panel.Controls.Add(Imagelabel);
            panel.Controls.Add(Pricelabel);
            panel.Controls.Add(Timelabel);

        }

        void AddButtonClick(TextBox IdBox, TextBox TitleBox, TextBox TimeBox, TextBox PriceBox,string FilePath)
        {
            MoviesWrappers.Add
                (new MoviesWrapper
                (int.Parse(IdBox.Text),
                TitleBox.Text, 
                TimeBox.Text, 
                Double.Parse(PriceBox.Text), 
                FilePath));
            MessageBox.Show(
                  MoviesWrappers.Last().FilePath + " "  
                + MoviesWrappers.Last().Id+ " "
                + MoviesWrappers.Last().Time+ " "
                + MoviesWrappers.Last().Title+ " "
                + MoviesWrappers.Last().Price + " "
                );
        }
        void BackButtonClick()
        {
            RemovePreviousPageComponents();
            AdminPageComponents();
        }
        void GetImage(string FilePath)
        {
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
                MessageBox.Show("Selected file path:\n" + FilePath);
            }
        }

        #endregion
        private void BookingPage_Click(object sender, EventArgs e)
        {
            RemovePreviousPageComponents();

            BookingPageComponents();
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
    }
}
