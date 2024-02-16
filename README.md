# PixelPlatformer / 2D 플랫포머

유니티를 통하여 2D 플랫포머 게임의 기초를 제작해보고 C#의 원리를 이해하기 위해 프로젝트 진행
PC 게임 HollowKnight 같은 움직임을 구현하기 위하여 환경 조정

-특징
모션에서 새로운 인스턴스 연동

공격 모션 1, 2, 3 분리
-죽음의 보스에서 생성되는 Ethan의 공격 모션은 총 3단계로 구성되어있으며 공격 횟수에 따라 모션이 변하는 것을 구현

대쉬 구현

더블점프 구현

보스 공격 패턴 구현

히트 포인트 구현 (canvas 이용)
-Secene 안에 Canvas를 생성하고 그 안에 HitPoint 텍스트를 만들어 캐릭터가 움직이더라도 HitPoint를 유지할수 있게 만듦

메인카메라 연동
-각 상황에 알맞는 메인카메라 움직임으로 특정상황에서는 좌표만 움직이고 다른 상황에서는 lerp로 부드럽게 움직이도록 변환

 
![첫화면](https://github.com/OhYunTaek123/PixelPlatformer/assets/128479666/8e62f590-89b1-45e7-81ef-a6321300a70f)

첫화면
-계속해서 움직이는 배경 제작
-대시 구현 <- RayCast 통해 대시 시도시 벽이나 object와 너무 가까우면 벽과 제일 가까운 거리만 이동
-공격 구현 <- BoxCast 통해 공격 버튼 입력시 이펙트의 거리만큼 공격 범위 구현
-움직임 구현 <- 더블점프 및 상하좌우 이동 공중 상태와 같은 각 모션의 animation을 적절하게 연결하여 논리를 구성함


![고스트보스 히트포인트](https://github.com/OhYunTaek123/PixelPlatformer/assets/128479666/9e7a23ac-5b3d-470d-bc8f-560e92129cd8)

고스트 보스
-일정 시간마다 맵의 곳곳을 순간이동하며 bullet을 쏘아 메인캐릭터가 히트시 우상단의 HitPoint가 1씩 오르게 함
-bullet Prefab은 중복이 계속하여 일어나기 때문에 하나만 만들고 Instantiate를 통하여 Clone을 생성함

![직접만들어 합친 패턴](https://github.com/OhYunTaek123/PixelPlatformer/assets/128479666/a737dad3-d0ff-4c72-99e6-f2c354d0e7b3)

죽음의 보스
-좌우로 계속 움직이다가 플레이어와 조우시 3가지 패턴중 하나를 랜덤 사용하게 제작
-보스에 prefab을 연결하고 해당 prefab에서 또다시 새로운 prefab을 연동하여 인스턴스화
-각 만들어진 이미지들은 특정 상황이 충족되면 사라지게 만듦

![필드몬스터](https://github.com/OhYunTaek123/PixelPlatformer/assets/128479666/9ecd9fbb-3bac-42fb-aef3-bdbcfebbeb8c)

필드를 돌아다닐수 있음
-일반 필드를 돌아다니다 만나는 토끼와 충돌하게 되면 색이변하고 addforce로 위로 튕기게 만듦
-토끼는 laycast를 본인 앞에서 생성하게 만들어 절벽 아래로 떨어지지 않게 만듦
