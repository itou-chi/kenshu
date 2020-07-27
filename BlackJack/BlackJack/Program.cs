using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            //カードの枚数(52枚)をリストに格納する（マークなどの細かい判別や計算はメソッドでする）
            var card = new List<int>();
            for (int i = 1; i <= 52; i++) {
                card.Add(i);
            }

            //カードをシャッフルする（リストの中身をランダムな順番にする）
            int[] shuffleCard = card.OrderBy(i => Guid.NewGuid()).ToArray();

            //プレイヤーとディーラーが最初の２枚カードを引く（手札は状況に応じて増えるからリスト）
            //山札のインデックス番号なのでプレイヤーとディーラーで番号が続いていることに注意
            var player = new List<int>();
            var dealer = new List<int>();
            player.Add(shuffleCard[0]);
            player.Add(shuffleCard[1]);
            dealer.Add(shuffleCard[2]);
            dealer.Add(shuffleCard[3]);
            

            //カードを見せる（ディーラーの2枚目は開示しない）
            //山札ではなく各手札の配列に格納したものをCardNameメソッドで整形して呼び出す
            Console.WriteLine("プレイヤーが引いた１枚目のカードは[" + CardName(player[0]) + "]です。");
            Console.WriteLine("プレイヤーが引いた２枚目のカードは[" + CardName(player[1]) + "]です。");
            Console.WriteLine("ディーラーが引いた１枚目のカードは[" + CardName(dealer[0]) + "]です。");
            Console.WriteLine("ディーラーが引いた２枚目のカードは秘密です。");

            //プレイヤーの手札の合計を表示する(CardSumメソッド)
            Console.WriteLine("プレイヤーの手札の合計は" + CardSum(player) + "です。");

            //手札を増やす際に加算していく数値。山札の配列は[3]まで使ったので4から
            int count = 4;

            //手札を増やす際に加算していく数値。各人の配列は[1]まで使ったので2から
            int pcount = 2;
            int dcount = 2;  //使われていないがプレイヤーに合わせて一応実装


            //2周目以降
            while (true)
            {

                //プレイヤーに続けるかどうか判断を求める
                Console.WriteLine();
                Console.Write("プレイヤーの番です。カードを引きますか？(yes or no) : ");
                //入力された内容を読み取る
                String answer = Console.ReadLine();

                //入力された文字列から続行か判断
                //引く場合
                if (answer.Equals("yes") || answer.Equals("Yes") || answer.Equals("YES") || answer.Equals("はい"))
                {
                    //プレイヤーがカードを引く
                    player.Add(shuffleCard[count]);
                    Console.WriteLine("プレイヤーが引いたカードは[" + CardName(player[pcount]) + "]です。");
                    count++;
                    pcount++;

                    //プレイヤーがバーストしたらその時点で終了（ディーラー勝ち）
                    if (CardSum(player) > 21)
                    {
                        Console.WriteLine();
                        Console.WriteLine("プレイヤーがバーストしました。ディーラーの勝ちです！");
                        Console.WriteLine("プレイヤー：" + CardSum(player) + "、ディーラー：" + CardSum(dealer));
                        return;
                    }

                    //ディーラーの手札の合計が17未満ならカードを引く
                    if (CardSum(dealer) < 17)
                    {
                        dealer.Add(shuffleCard[count]);
                        Console.WriteLine("ディーラーがカードを引きました。");
                        count++;
                        dcount++;
                    }
                    else
                    {
                        Console.WriteLine("ディーラーがパスしました。");
                    }

                    //ディーラーがバーストしたらその時点で終了（プレイヤー勝ち）
                    if (CardSum(dealer) > 21)
                    {
                        Console.WriteLine();
                        Console.WriteLine("ディーラーがバーストしました。プレイヤーの勝ちです！");
                        Console.WriteLine("プレイヤー：" + CardSum(player) + "、ディーラー：" + CardSum(dealer));
                        return;
                    }

                    //プレイヤーの手札の合計を表示する(CardSumメソッド)
                    Console.WriteLine("プレイヤーの手札の合計は" + CardSum(player) + "です。");

                }

                //引かない場合（勝敗を決める）
                else if (answer.Equals("no") || answer.Equals("No") || answer.Equals("NO") || answer.Equals("いいえ"))
                {
                    //ディーラーの手札の合計が17未満ならカードを引く
                    while (CardSum(dealer) < 17)
                    {
                        dealer.Add(shuffleCard[count]);
                        Console.WriteLine("プレイヤーがパスしました。");
                        Console.WriteLine("ディーラーがカードを引きました。");
                        count++;
                        dcount++;
                    }

                    //ディーラーがバーストしたらその時点で終了（プレイヤー勝ち）
                    if (CardSum(dealer) > 21)
                    {
                        Console.WriteLine();
                        Console.WriteLine("ディーラーがバーストしました。プレイヤーの勝ちです！");
                        Console.WriteLine("プレイヤー：" + CardSum(player) + "、ディーラー：" + CardSum(dealer));
                        return;
                    }

                    //それぞれの手札の合計を表示
                    Console.WriteLine();
                    Console.WriteLine("手札の合計は・・・");
                    Console.WriteLine("プレイヤー：" + CardSum(player) + "、ディーラー：" + CardSum(dealer));

                    //手札の合計と21の差が小さいほうが勝ち
                    if (21 - (CardSum(player)) < (21 - CardSum(dealer)))
                    {
                        Console.WriteLine("プレイヤーの勝ちです！");
                        return;
                    }
                    else if (21 - (CardSum(player)) > 21 - (CardSum(dealer)))
                    {
                        Console.WriteLine("ディーラーの勝ちです！");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("引き分けです！");
                        return;
                    }
                }
                //入力内容が上記に当てはまらないとき、入力しなおし
                else
                {
                    Console.WriteLine("入力した内容ではゲームの続行を判断できません。");
                    Console.WriteLine("もう一度入力してください。");
                }
            }


        }

        //カードの番号からマークを割り当てるメソッド
        //13で割って商が0ならスペード、1ならクラブ、2ならダイヤ、3ならハート
        //ズレが出ないように-1してインデックス番号として処理
        static String ChangeMark(int number) {
            switch ((number - 1) / 13) {
                case 0:
                    return "スペードの";
                case 1:
                    return "クラブの";
                case 2:
                    return "ダイヤの";
                case 3:
                    return "ハートの";
                default:
                    return "（マーク判別不可）";
            }
        }

        //カードの番号から数字を割り当てるメソッド
        //13で割った余りに応じて1～13を割り当てる(0の時は13)
        //基準（これをもとに表示用、計算用のメソッドをつくる）
        static int ChangeNumber(int number) {
            int changeNumber = number % 13;
            if (number % 13 == 0) {
                changeNumber = 13;
            }
            return changeNumber;  
        }

        //ChangeNumberメソッドをもとに文字を割り当てるメソッド
        //数字が1ならA、11ならJ、12ならQ、13ならK、それ以外はその数字のまま
        //表示用
        static String ChangeString(int changeNumber) {
            switch (changeNumber) {
                case 1:
                    return "A";
                case 2:
                    return "2";
                case 3:
                    return "3";
                case 4:
                    return "4";
                case 5:
                    return "5";
                case 6:
                    return "6";
                case 7:
                    return "7";
                case 8:
                    return "8";
                case 9:
                    return "9";
                case 10:
                    return "10";
                case 11:
                    return "J";
                case 12:
                    return "Q";
                case 13:
                    return "K";
                default:
                    return "（数字判別不可）";
            }
        }

        //ChangeMarkメソッドとChangeStirngメソッドを合わせてカードを指定するメソッド
        //表示用
        static String CardName(int number) {
            String mark = ChangeMark(number);
            String str = ChangeString(ChangeNumber(number));
            String cardName = mark + str;
            return cardName;
        }

        //手札の合計を算出するメソッド
        //ChangeNumberメソッドをもとに11,12,13を10に変更する
        static int CardSum(List<int> tefuda) {
            int sum = 0;
            for (int i = 0; i < tefuda.Count(); i++) {
                int ten = ChangeNumber(tefuda[i]);
                if (ten == 11 || ten == 12 || ten == 13) {
                    ten = 10;
                }
                sum += ten;
            }
            return sum;
        }


    }
}
