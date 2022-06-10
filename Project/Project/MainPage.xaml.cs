using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Diagnostics;

namespace Project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
 

    public partial class MainPage : ContentPage
    {

        // 시간 상수
        public const int second = 1000;
        public const int minute = second * 60;
        public const int hour = minute * 12;


        public MainPage()
        {
            InitializeComponent();
            GetDate();
            GetTime();
            NewsMarqueeEffect();
            ChangeAnalogClock();
            ShowStockInfo();
            RingKakaoTalk();
            SetDayAndNight();
            ChangeBackground();
        }


 
        // 배경화면 변경 기능
        private async void ChangeBackground()
        {
            int background_idx = 0;
            string[] background_arr = { "background1.png", "background2.jpg", "background3.jpg", "background4.jpg" };
            while (true)
            {
                await background_image.FadeTo(0, second);
                background_idx++; background_idx %= 4;
                background_image.Source = background_arr[background_idx];
                await background_image.FadeTo(1, second);
                await Task.Delay(10 * second);
            }
        }



        /* 날짜 기능 */
        private async void GetDate()
        {
            DateTime data = DateTime.Today;
            date_label.Text = data.ToString("yyyy-MM-dd\ndddd");
        }



        /* 디지털 시계 설정과 스톱워치 기능 */
        DateTime exp_time;
        private async void GetTime()
        {

            while (true)
            {
                DateTime time = DateTime.Now;
                digital_clock_time.Text = time.ToString("HH:mm:ss");
                miniclock_label.Text = time.ToString("HH : mm : ss");

                if (stopwatch_toggle.Text == "Pause")
                {
                    var diff = time.Subtract(exp_time); 
                    stopwatch_time.Text = diff.ToString("mm\\:ss\\:ff");
                }

                await Task.Delay(100);
            }
        }

        private void toggle_Clicked(object sender, EventArgs e)
        {
            if (stopwatch_toggle.Text == "Pause") stopwatch_toggle.Text = "Start";
            else
            {
                stopwatch_toggle.Text = "Pause";
                exp_time = DateTime.Now;
            }
        }



        /* 아날로그 시계 기능 */
        private async void ChangeAnalogClock()
        {
            // 1초에 360/60도 = 6도
            // 1분에 360/60도 = 6도
            // 1시간에 360/12 = 30도

            while (true)
            {
                hour_hand.RotateTo(DateTime.Now.Hour * 30, 10);
                min_hand.RotateTo(DateTime.Now.Minute * 6, 10);
                sec_hand.RotateTo(DateTime.Now.Second * 6, 10);
                await Task.Delay(100);
            }
        }

        

        /* 시간에 따라 세계의 낮과 밤 보여주는 기능 */
        private async void SetDayAndNight()
        {
            int selector = DateTime.Now.Hour;

            if (selector % 2 == 1)
            {
                selector -= 1;
            }

            map_image.Source = selector.ToString() + ".png";
        }



        /* 증권 소식 보여주는 기능 */
        private async void ShowStockInfo()
        {
            int idx = 0;
            string[,] stock_arr = { { "KTOP 30", "8,896.59 ▲ 30.31 (0.34)" },
                                    { "KOSPI", "2,617.22 ▲ 11.35 (0.44)" },
                                    { "KOSPI 200", "345.60 ▲ 1.52 (0.44)" },
                                    { "KOSDAQ", "872.69 ▲ 7.62 (0.88)" },
                                    { "KOSDAQ 150", "1,245.64 ▲ 17.78 (1.45)" },
                                    { "KRX 300", "1,575.86 ▲ 8.34 (0.53)" } };
            while (true)
            {
                await Task.WhenAny<bool>
                (
                    stock_info_layout.FadeTo(0, second / 2),
                    stock_info_layout.TranslateTo(0, -50, second, Easing.Linear)
                );

                idx++; idx %= stock_arr.Length / 2;
                stock_item_label.Text = stock_arr[idx, 0];
                stock_data_label.Text = stock_arr[idx, 1];

                await stock_info_layout.TranslateTo(0, 50, 0);

                await Task.WhenAny<bool>
                (
                    stock_info_layout.FadeTo(1, second / 2),
                    stock_info_layout.TranslateTo(0, 0, second / 2, Easing.Linear)
                );

                await Task.Delay(5 * second);
            }
        }



        /* 최신 뉴스가 흘러가는 기능 */
        private async void NewsMarqueeEffect()
        {
            int idx = 0;
            string[] news_arr = { "현대차, '안전벨트 부품' 문제로 미국서 23만 9천 대 리콜",
                                  "대기업 줄줄이 \"거액 국내 투자\" 발표",
                                  "손흥민, '황금 신발' 들고 금의환향…계속되는 찬사",
                                  "WHO 사무총장 연임 확정, 임기 2027년까지",
                                  "'헤어질 결심' 박해일 \"인생에서 이런 역할 있었나 싶었죠\"",
                                  "'원숭이 두창' 해외 유입 막는다…검역 · 검사 강화",
                                  "화이자 \"북한 등 45개 빈곤국에 백신 '원가' 공급\""};
            while (true)
            {
                idx++; idx %= news_arr.Length;
                news_label.Text = news_arr[idx];
                await news_label.TranslateTo(-news_label.Width, 0, 0);
                await news_label.TranslateTo(1920 - 250, 0, 13 * second);
            }
        }


        /* 넷플릭스와 유튜브 어플 누르는 기능 */
        string netflix_site = "netflix_site.PNG";
        string youtube_site = "youtube_site.PNG";
        private void netflix_Clicked(object sender, EventArgs e)
        {
            app_image.Source = netflix_site;
            app_title_label.Text = "N  E  T  F  L  I  X";
            app_layout.IsVisible = !app_layout.IsVisible;
            if (!app_layout.IsVisible) app_image.Source = null;
        }

        private void youtube_Clicked(object sender, EventArgs e)
        {
            app_image.Source = youtube_site;
            app_title_label.Text = "Y  O  U  T  U  B  E";
            app_layout.IsVisible = !app_layout.IsVisible;
            if (!app_layout.IsVisible) app_image.Source = null;
        }



        /* 일일 물 섭취량 위젯 기능 */
        int water_cnt = 0;
        private void water_Clicked(object sender, EventArgs e)
        {
            water_cnt++;
            if (water_cnt == 6)
            {
                water_cnt = 0;
                for(int i=1; i<=5; i++)
                {
                    Image temp = (Image)FindByName("water" + i.ToString());
                    temp.Source = "water2.png";
                }
            }
            else
            {
                Image temp = (Image)FindByName("water" + water_cnt.ToString());
                temp.Source = "water1.png";
            }

        }



        /* 카카오톡 알림 울리는 기능 */
        private async void RingKakaoTalk()
        {
            kakaotalk_layout.IsVisible = true;

            int idx = 0;
            string[,] messages_arr = { { "모바일SW", "봉투가 도착했어요. 1원을 받으세요." },
                               { "한국장학재단", "2학기 국가장학금 신청 안내드립니다." },
                               { "배달의민족", "(픽업완료) 고객님이 주문하신 배달이 시작되었습니다." },
                               { "카카오톡", "[PC버전 자동로그인]" },
                               { "행운의편지", "이 편지는 영국에서 최초로 시작되어 일년에 한 바퀴 돌면서…" },
                               { "고양이", "RRRRRRRRRRRR@#$RTYUIO(P)ddkkkkkkkkk" } };

            while (true)
            {
                idx++; idx %= messages_arr.Length / 2;
                kakaotalk_friend_label.Text = messages_arr[idx, 0];
                kakaotalk_message_label.Text = messages_arr[idx, 1];

                await kakaotalk_layout.FadeTo(1, second, Easing.CubicInOut);
                await Task.Delay(3 * second);
                await kakaotalk_layout.FadeTo(0, second, Easing.CubicInOut);
                await Task.Delay(5 * second);
            }
        }
    }
}