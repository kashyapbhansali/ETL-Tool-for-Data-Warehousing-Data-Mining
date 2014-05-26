import java.io.*;
import java.util.*;
class apriori 
{
   public static void main(String args[]) throws IOException
    {
        BufferedReader br =new BufferedReader(new InputStreamReader(System.in));
        int i,j,k,l=0;
        String output="";
        String prod[][]=new String[50][50];
        String[][] trans=new String[50][50];
        trans[0][0]="t1";
        trans[0][1]="Swimming,SPA,Gym";
        trans[1][0]="t2";
        trans[1][1]="Swimming,Gym";
        trans[2][0]="t3";
        trans[2][1]="Swimming,Casino,Gym";
        trans[3][0]="t4";
        trans[3][1]="Bar,Swimming";
        trans[4][0]="t5";
        trans[4][1]="Bar,Casino";
       
        int n=5;
        int stage=0;
        float sup=0.3F,con=0.7F;
        k=0;
        for(i=0;i<n;i++)
        {
            String aa[]=trans[i][1].split(",");
            for(j=0;j<aa.length;j++)
            {
                Boolean flag=false;
                for(l=0;l<k;l++)
                {
                    if(aa[j].equals(prod[l][0]))
                    {
                        flag=true;
                        break;
                    }
                }
                if(!flag)
                {
                    prod[l][0]=aa[j];
                    k++;
                }
            }
        }
        String large[][]=new String[50][50];
        int final1=0;;
        boolean flag1=true;
        while(flag1)
        {
            
            int mm=0;
            l=0;
            
            boolean flag=false;
            output+="stage="+stage+"\n";
            output+="all product are "+k+"\n";
             System.out.println("stage="+stage);
                System.out.println("all product are "+k);
            for(i=0;i<k;i++)
            {
                String am=prod[i][0];
                System.out.println(am+"ok");
                output+=am+"ok"+"\n";
               if(am!=null)
                {
                    prod[i]=am.split(",");
               }
               else{
                    i--;
                    k--;
                    output+="its null"+"\n";
                    System.out.println("its null");
               }
            }
                int nn=k;
                final1=k;
                System.out.println("new value of k"+k);
                output+="new value of k"+k+"\n";
                for(i=0;i<nn;i++)
                {
                    mm=0;
                    for(j=0;j<n;j++)
                    {
                        flag=true;
                        for(k=0;k<(stage+1);k++)
                        {
                            if(prod[i][k]!=null)
                            if(!trans[j][1].contains(prod[i][k]))
                            {
                                flag=false;
                            }
                        }
                        if(flag==true)
                        {
                            mm++;
                          
                        }
                        
                    }
                        System.out.println(mm);
                        output+=mm+"\n";
                    if(mm>=Math.ceil(n*sup))
                    {
                        for(k=0;k<(stage+1);k++)
                        {
                            large[l][k]=prod[i][k];

                        }
                        l++;
                    }
                }
                System.out.println("");
                output+="\n";
                final1=l;
                output+="all large item set are "+l+"\n";
                System.out.println("all large item set are "+l);
                for(i=0;i<l;i++)
                {
                    for(k=0;k<(stage+1);k++)
                    {
                        System.out.print(large[i][k]+" ");
                    output+=large[i][k]+" ";

                    }
                    output+="\n";
                    System.out.println("");
                }

                String[] ab;
                int common=0;
                int m=0;
                
                for(i=0;i<(l-1);i++)
                {
                        boolean fl=false;
                        for(int a=i+1;a<(l);a++)
                        {
                             common=0;
                             for(j=0;j<(stage+1);j++)
                            {
                           
                                for(int b=0;b<(stage+1);b++)
                                {
                                    if(large[i][j].equals(large[a][b]))
                                    {
                                        common++;

                                    }
                                }
                             }
                            if(common==stage)
                            {
                                String aaa="",aab="";
                                for(int b=0;b<(stage+1);b++)
                                {
                                    if(!aab.contains(large[i][b]))
                                    aaa+=large[i][b]+",";

                                    if(!aaa.contains(large[a][b]))
                                    aab+=large[a][b]+",";
                                }
                                prod[m][0]=aaa+aab;
                                m++;
                                fl=true;
                               
                            }
                    }
               }
                output+="to next stage";
               System.out.println("To next stage");
                for(i=0;i<m;i++)
                {
                    output+=prod[i][0]+"\n";
                    System.out.println(prod[i][0]);
                }
                String[][] tp=new String[m][50];
               for(int u=0;u<m;u++)
               {
                   for(int v=0;v<(stage+1);v++)
                   {
                       tp[u]=prod[u][0].split(",");
                   }
               }
                int w;
                stage++;
                int equal[]=new int[50];
                int count=0;
                for(int u=0;u<m;u++)
                {
                    count=0;
                    boolean flag12=false;
                      for(w=u+1;w<m-1;w++)                                      
                      {
                       for(int v=0;v<(stage+1);v++)
                        {
                            if(prod[w][0]!=null)
                            if(!prod[w][0].contains(tp[u][v]))
                            {
                                flag12=true;
                                break;
                            }
                            
                        }
                       if(!flag12)
                        {
                            prod[w][0]=null;
                        }
                    }
                    
                }
                if(m==0)
                {
                    flag1=false;
                }
                k=m;
        }
        output+=stage+"\n";
        System.out.println(stage);
        output+=final1+"\n";
        System.out.println(final1);
        output+=large[0][0]+"\t"+large[0][1]+"\n";
        System.out.println(large[0][0]+"\t"+large[0][1]);
       boolean flag=false;
       int count[]=new int[final1];
       String str[]=new String[final1];
       for(int z=0;z<n;z++)
       {
            for(int u=0;u<final1;u++)
            {
                flag=true;
                for(int v=0;v<stage;v++)
                {
                    if(!trans[z][1].contains(large[u][v]))
                    {
                        flag=false;
                        break;
                    }
                }
                if(flag)
                {
                    count[u]++;
                }
            }
       }
       for(int u=0;u<final1;u++)
       {
           str[u]="";
           for(int v=0;v<stage;v++)
           {
               str[u]+=large[u][v]+",";
           }
       }
       for(int u=0;u<final1;u++)
       {
           for(int v=0;v<stage;v++)
           {
               int count1=0;
               for(int w=0;w<n;w++)
               {
                   if(trans[w][1].contains(large[u][v]))
                   {
                       count1++;
                   }
               }
               float div=((float)count[u]/count1);
               output+=count[u]+"  "+count1+"\n";
               System.out.println(count[u]+"  "+count1);
               output+=div+"\n";
               System.out.println(div);
               if(div>=con)
               {
                   output+=large[u][v]+"=>"+str[u]+"\n";
                   System.out.println(large[u][v]+"=>"+str[u]);
               }
           }
       }
    }
}