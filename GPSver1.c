#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <sys/types.h>
#include <unistd.h>
#include <sys/wait.h>


typedef struct GpsRoadPoints
       {
       float latitude;
       float longitude;
       int max_km_phours;
       char roadType[30];
       } GRP;

typedef struct ListOfPoints
       {
       GRP *gPoint; 
       struct ListOfPoints *next;  
       } LOP;
       

void dealocate(LOP** root){
       LOP* curr=*root;
       LOP* next;
       while(curr!=NULL){
             next=curr;
             curr=curr->next;
             free(next->gPoint);
             free(next);
       }
       *root=NULL;
}

void insert_end(LOP** root,float lat,float lon,int maxKm, char road[30]){
       LOP *new_node= malloc(sizeof(LOP));
       if(new_node==NULL){
              exit(1);
       }
       new_node->gPoint= malloc(sizeof(GRP));
       new_node->next=NULL;
       new_node->gPoint->latitude=lat; 
       new_node->gPoint->longitude=lon;
       new_node->gPoint->max_km_phours=maxKm;
       strcpy(new_node->gPoint->roadType,road);
  
       if(*root==NULL){
              *root=new_node;
              return;
       }
       LOP* curr=*root;
       while (curr->next!=NULL)
       {
              curr=curr->next;
       }
       curr->next=new_node;
}

void WriteListToFile(LOP** root){
       FILE *file;
       file=fopen("gpsPoints.bin","wb");
       if(file!=NULL){
              LOP* curr=*root;
              LOP* currNext=NULL;
       while (curr!=NULL)
       {
              currNext=curr->next;
              curr->next=NULL;
              fseek(file,0,SEEK_END);
              fwrite(curr->gPoint,sizeof(GRP),1,file);
              printf("Writing:%s point to file\n",curr->gPoint->roadType);
              curr->next=currNext;
              currNext=NULL;
              curr=curr->next;
       }
       fclose(file);
       file=NULL;
       dealocate(root);
       }else{
              dealocate(root);
              printf("Error at writing");
              exit(1);
       }
}



void ReadFile(LOP** root){
       int i=0;
       FILE *file;
       file=fopen("gpsPoints.bin","rb");
       if(file!=NULL){
              //dealocate(root);
              fseek(file,0,SEEK_END);// nameri kraq na file s 0bit ofset
              long filesize=ftell(file);//kolgo e dulgo ot markera do nachaloto
              rewind(file);//nachalo
              long numberOfGrps= (long)(filesize/(sizeof(GRP)));
              printf("Entries:%ld\n",numberOfGrps);
       for(i=0;i<numberOfGrps;i++){
            //fseek(file,(sizeof(GRP)*i),SEEK_SET);
              if(*root==NULL){
                     *root=malloc(sizeof(LOP));
                     (*root)->gPoint= malloc(sizeof(GRP));
                     fread((*root)->gPoint,sizeof(GRP),1,file);
                     (*root)->next=NULL;
                     
              }else{
                     LOP* curr=*root;
                     LOP* newNode=malloc(sizeof(LOP));
                     newNode->gPoint= malloc(sizeof(GRP));
                     while ((curr->next!=NULL))
                     {
                            curr=curr->next;
                     }
                     fread(newNode->gPoint,sizeof(GRP),1,file);
                     curr->next=newNode;
                     newNode->next=NULL;        
              }
            //ReadNext(&root,file);

       }
              

}else{
printf("Error reading");
}          
fclose(file);
}

int main(){
       int id;
       LOP* root=NULL;
       id=fork();
	   
       if(id==0){
              insert_end(&root,1,2,3,"highway");
              insert_end(&root,5,6,7,"Bulevard");
              insert_end(&root,8,9,10,"Bulevard");
              insert_end(&root,11,12,13,"Street");
              insert_end(&root,15,16,17,"Bulevard");
              insert_end(&root,18,19,20,"Street");
              insert_end(&root,21,22,23,"highway");
              insert_end(&root,25,26,27,"Bulevard");
              insert_end(&root,28,29,30,"Mountain road");
              WriteListToFile(&root);
              dealocate(&root);
       }else{    
              wait(NULL);  
              ReadFile(&root);
       for(LOP* curr=root;curr!=NULL;curr=curr->next){
              printf("First: %f\n",curr->gPoint->latitude);
              printf("Second: %f\n",curr->gPoint->longitude);
              printf("Third: %d\n",curr->gPoint->max_km_phours);
              printf("Forth: %s\n",curr->gPoint->roadType);
}    
       dealocate(&root);
}   
       return 0;
}