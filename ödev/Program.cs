using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _23._11._2022Ödev
{

	internal class Program
	{
		class Block
		{
			public int data;
			public Block next;
			public Block prev;
		}
		static int[] stuck = new int[100];
		static Block Sp = null;

		static void push(int data)//optimize edilmiş hali
		{
			Block b1 = new Block();
			b1.data = data;
			b1.next = null;
			b1.prev = null;

			if (Sp != null)
			{
				Sp.prev = b1;
				b1.next = Sp;
			}
			Sp = b1;
		}

		static int Pop()
		{
			int data = Sp.data;
			Sp = Sp.next;
			return data;
		}
		static int peek()//pop yapmadan stuck dan okuma için
		{
			return Sp.data;
		}

		static void Main(string[] args)
		{
			#region
			//posfix ifadeyi infix e dönüştür ve check et
			//x^a veya x^c gibi üslü ifadeleri de ekle x^b*c --> xb^c*
			//2.ödev a b c nin 1 olduğu durum
			// infix = "alan/(boyut*derinlik-f*g/a-a/b+c)"

			//ilk harfi infixe yaz 2.temp e at işaret gelince işareti al sonra tempdekini yaz
			//index of u kullan


			//ilk işaret gelince işaretden önceki ikisine işlemi uygula parantez at
			//(c*d) c ye gelene kadar tekrar oku c den öncekini parantezle başla 2.işaretde
			//
			//işarete geldim ondan 2 öncesini c yi infixe attım başına ( attım sonra 1. işareti infixe attımsonra 
			//işaretten öncekini attım sonra ) kapadık sonra c nin sırasından 1 öncekinin başına parantez ve c den
			//önceki b yi sonra 2. işareti attım sonra en dışa 1 parantez daha sonra b den öncekini başına ( atıp 
			//infixe kaydettim sonra eon işareti attım ve bitirdim $ işaretini ve infixi kullan

			//posfixte parantez olmaz
			//3 lü kontrol metodu
			#endregion

			#region posfixten infixe arada ufak hata var
			push((byte)'$');
			string posfix = "acb*-ad*/";//cb*ad*/  veya  ca/b*cd*-  acb*-ad*/
			string infix = "";
			string op = "$(*/+-)";
			string temp1 = "", temp2 = "";
			int sayaç = 0;
			for (int i = 0; i < posfix.Length; i++)
			{
				if (op.IndexOf(posfix[i]) == -1)// değ mi 
				{
					push((byte)posfix[i]);
					sayaç = 0;
				}
				else//adc**
				{
					if (sayaç >= 1 && infix != " ")//üst üste 2 kez işaret gelmiş abc**-->b*c
					{
						if (peek() != (byte)'$')
						{

							infix += posfix[i - 2];
							infix += (char)Pop();


							//temp1 += (char)Pop();
							//temp1 += posfix[i];//temp1 de a*var infix de b*c

							//temp2 = temp1;
							//temp2 += infix;
							//infix = temp2;
						}
						//pop yazıp ilk değ önce inf e sonra işaret sonra inf baş değeri
						//temp = infix;
						//infix = "";
						//infix += (char)Pop();
						//infix+=posfix[i];
						//infix += temp;
					}

					else
					{
						infix += (char)Pop();
						infix += posfix[i];
						if (peek() != (byte)'$')
						{
							infix += (char)Pop();//en sonda
						}
						sayaç++;
					}
				}

			}
			Console.WriteLine(infix);
			#endregion




			#region
			//posfix 0 1 op den farklı ve 2 de op ise düzenleyiip yaz
			//değilse i artsın bc- yazdı i+3 de işaret varsa bc nin başına parantez ekle


			//if (op.IndexOf(posfix[i - 2]) == -1 && op.IndexOf(posfix[i - 1]) == -1 && op.IndexOf(posfix[i]) == 0)//bulursa
			//{
			//	infix += posfix[i - 2] + posfix[i] + posfix[i - 1];
			//	if (op.IndexOf(posfix[i + 1]) == -1 && op.IndexOf(posfix[i + 2]) != -1)
			//	{
			//		infix += posfix[i + 1] + posfix[i + 2];
			//		continue;
			//	}
			//}
			#endregion
			#region çözüm denemesi 2
			//if (op.IndexOf(posfix[i]) == -1)//değişken ise
			//{
			//	infix += posfix[i];

			//}
			//else//işaret gelir ise
			//{

			//}
			#endregion
			#region
			//if (op.IndexOf(posfix[i]) == -1)//2.değ mi
			//{
			//	if (op.IndexOf(posfix[i]) != -1)//3.işaret mi abc**
			//	{
			//		infix += posfix[i - 2];
			//		infix += posfix[i];
			//		infix += posfix[i - 1];
			//		i += 3;

			//	}
			//	else//3.de değ ise 
			//	{
			//		continue;//ilerle 1 tane

			//	}
			//}
			//else//2. nin işaret olduğu durum
			//{
			//	infix += posfix[i];
			//	infix += posfix[i - 1];
			//	i++;
			//	//2 tane atladı
			//	//if (op.IndexOf(posfix[i+1]) == -1&& op.IndexOf(posfix[i + 2]) = -1)//3.ve 4.değ ise onlardan sonraki işaret araya gelecek ab*c*da
			//	//{//}

			//}
			#endregion


			#region infixten posfixe 
			//push('$');
			//string infix = "a*(b-c)*d";
			//string posfix = "";
			//string op = "$(*/+-)";//stuck boşken işlem yapamayız stuck ın baş
			//					  //değeri atanan değer ile kontrol edilir $ işareti kullanılır genelde
			//string öncelik = "0022110";
			//for (int i = 0; i < infix.Length; i++)
			//{
			//	if (op.IndexOf(infix[i]) == -1)//op nin içinde infix in isini ara bulmazzsa -1
			//	{
			//		//bulamadığı durumda yaz
			//		posfix += infix[i];
			//		continue;//else yazmamak için
			//	}
			//	if (infix[i] == '(')//bir de sağ parantez kısmı var
			//	{
			//		push((byte)'(');//stuck a sol parantez pushla
			//		continue;
			//	}
			//	if (infix[i] == ')')
			//	{
			//		while (peek() != (byte)'(')//sol paranteze gelene kadar stuckdakileri posfix e yaz
			//		{
			//			posfix += (char)Pop();
			//		}
			//		Pop();//sol parantezi pushlamıyo üsdeki ifade sol parantezi silmek için
			//		continue;
			//	}

			//	int l = (byte)peek();//************************************************************************
			//	l = op.IndexOf((char)l);//l nin durumları i=1 l=0/
			//	if (öncelik[l] >= öncelik[op.IndexOf(infix[i])])
			//	//stucktan alınan op nin önceliği=mevcut gelen infixten gelen op nin öncelik durumu
			//	{
			//		posfix += (char)Pop();
			//		push(infix[i]);
			//	}
			//	else
			//	{
			//		push(infix[i]);
			//	}
			//}
			//while (peek() != (byte)'$')
			//{
			//	posfix += (char)Pop();
			//}
			//Console.WriteLine(posfix);
			#endregion posfix ten infixe





		}
	}
}


