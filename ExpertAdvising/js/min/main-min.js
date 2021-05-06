var dd;
let body;
let eyeopen;
let eyeclosed;
let hair;
let dress;
let flag=0;
let glassesFlag=0;
let dressFlag=0;
let bodyFlag=0;

//loads the variable passed from url
function initDD(dddd,number2){
  dd = dddd;
  n2=number2;
  console.log(dd);
  console.log(n2);
} 

initDD();
//preloads all the images to prevent lagging
function preload(){

//BODY 
  body=loadImage('modelbody.png');
  bodyY=loadImage('modelbodyY.png');

  //EYES
  eyeopen=loadImage('eyeopen.png');
  eyeclosed=loadImage('eyesclosed.png');

  //HAIR
  hair1=loadImage('hair2.png');
  hairicon1=loadImage('hair2.png');
  hair2=loadImage('hair3.png');
  hairicon2=loadImage('hair3.png');

  //GLASSES
  glasses1=loadImage('glasses1.png');
  glassesicon1=loadImage('glasses1.png');
  glasses2=loadImage('glasses2.png');
  glassesicon2=loadImage('glasses2.png');
  glasses3=loadImage('glasses3.png');
  glassesicon3=loadImage('glasses3.png');


  //DRESSES
  dress=loadImage(dd);
  dressicon=loadImage(n2);


  //ICONS
  rightarrow=loadImage('rightarrow.png');
  leftarrow=loadImage('leftarrow.png');
}

//canvas size
function setup() {
  canvas=createCanvas(1500, 1100);
  canvas.position(650,100);

  //BUTTON
  button=createButton('Reset');
  button.size(150,60);
  button.position(830,1120);
  button.mousePressed(reset);

}

function reset()
{
  flag=0;
  glassesFlag=0;
  dressFlag=0;
  bodyFlag=0;
}

let rightarrow;
let leftarrow;
function arrow()
{
  image(rightarrow,410,880);
  rightarrow.resize(50,50);
  image(leftarrow,50,880);


}


var i=230;//Blinks in every 230 interval
function draw() {
  background(220);
  arrow();

//BODY
if(bodyFlag==0){
  image(body,0,0);
}else{
  image(bodyY,0,0);
}




  //DRESS
  if(dressFlag==1){
  image(dress,-45,0);
}
  textSize(50);
  text('Outfit', 530, 650);

  rect(550,700,150,150,25,255);
  image(dressicon,500,665);
  dressicon.resize(250,220);
  

//HAIR
    textSize(50);
    text('Hair', 530, 90);
    noStroke();
    rect(530,120,150,150,25,255);
    image(hairicon1,495,100);//the hair option
    hairicon1.resize(200, 200);
   

      
      
    noStroke();
    rect(770,120,150,150,25,255);
    image(hairicon2,785,110);//the hair option
    hairicon2.resize(120, 170);
   
  if(flag == 1)
  {
	  
	image(hair2,100,-5);
	  
  }
  else if(flag ==2)
  {
      image(hair1,70,-5);
  }



  
  //BLINKING
  
  i=i-1;
  
  if(bodyFlag==0){
  if(i<20){
    image(eyeclosed,113,63)
    if(i==0)
    {
      i=230;
    }
  }

  else{
      image(eyeopen,83,84);  
  }
}

  //GLASSES

    textSize(50);
    text('Glasses', 530, 350);
    
    var x=550;
    for(loop=0;loop<3;loop++)
    {
       rect(x,420,150,150,25,255);
       x=x+250;
    }

    image(glassesicon1,550,420);
    image(glassesicon2,800,430);
    image(glassesicon3,1030,410);


 
if (glassesFlag==1) {
    image(glasses1,165,110);
}
else if (glassesFlag==2) {
    image(glasses2,173,122);
}
else if (glassesFlag==3) {
    image(glasses3,150,102);
}


}


  function mousePressed(){
    
 //Hair   
  if(mouseX>=770 && mouseX<=920 && mouseY>=20 && mouseY<=270){
    
	flag = 1;
	

  }
    
    else if(mouseX>=530 && mouseX<=680 && mouseY>=20 && mouseY<=270){
		
		flag = 2;
		
    }

 
 //Glasses
  
  if(mouseX>=550 && mouseX<=750 && mouseY>=420 && mouseY<=570)
  {
    glassesFlag=1;
  }
 
  else if(mouseX>=800 && mouseX<=950 && mouseY>=420 && mouseY<=570)
  {
   glassesFlag=2; 
  }
  else if(mouseX>=1050 && mouseX<=1200 && mouseY>=420 && mouseY<=570)
  {
    glassesFlag=3;
  }

 //Dress

 if(mouseX>=550 && mouseX<=700 && mouseY>=700 && mouseY<=850)
 {
  dressFlag=1;
 }

//BODY
if(mouseX>=410 && mouseX<=460 && mouseY>=880 && mouseY<=930)
{
  
  if(bodyFlag==0)
  {
    bodyFlag=1;
  }
  if(bodyFlag==1)
  {
    bodyFlag=0;
  }
}



}