import java.util.Scanner;

public class MorningWorld{
        public static void main(String[] args){
                System.out.println("おはようございます");
                Scanner scan = new Scanner(System.in);
                System.out.print("input>");
                String str = scan.nextLine();
                System.out.print("input2>");
                String str2 = scan.nextLine();
                System.out.println(str);
                System.out.println(str2);
                scan.close();
                //
                //
        }
}
