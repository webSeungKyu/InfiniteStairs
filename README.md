# InfiniteStairs
 무한의 계단 모작<br><br>

[플레이 영상]<br>



https://github.com/webSeungKyu/InfiniteStairs/assets/112837427/8dabf080-2062-4e7c-b9bc-40ecf4f9dba8



<br>

※ 새로 배운 것 

<화면 해상도 설정>

[ 코드로 설정 ]
 - [가로], [세로], [풀스크린] / [창모드] 
 - Screen.SetResolution(1080, 1920, true); 

[ UI (Canvas) ]
 - [Sanvas Scaler] > [UI Scale Mode] : Scale With Screen Size
 - [Reference Resolution] : X, Y 해상도 설정 
[ Screen Math Mode : Match Width Or Height ] 고정 후 Match를 통해 상세 조정 가능


 -< Dictionary >
 - [정렬]
 - OrderBy(item => item.Key)

<정수형으로 변환>

[ Convert.ToInt32 ]
 - null은 0으로 변환
 - [char], [string] 모두 사용 가능

[ Int32.Parse ]
 - null은 변환 불가
 - [string]만 가능하며 [char]은 불가능

<이름 설정>
 - [ Instantiate().name ]
 - 무한으로 생성하는 오브젝트의 이름을 다르게 설정함

<범위 설정>
 - Image 혹은 버튼에 있는
 - [Image] : Raycast Padding 조정
