import java.util.Scanner;

public class HelloWorld{
        public static void main(String[] args){
                System.out.println("Hello, world.");
                System.out.println("Evening, world.");
                try{
                        Thread.sleep(5000);
                }
                catch(Exception ex){
                }
                System.out.println("リターンしても終わらない");
        }
}
