# 🎮 Dark Revenger (다크 리벤저)

![unity](https://img.shields.io/badge/unity-2022.3.23f1-black?style=for-the-badge&logo=unity&logoColor=white)
![csharp](https://img.shields.io/badge/c%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![platform](https://img.shields.io/badge/platform-PC%20%2F%20Windows-blue?style=for-the-badge)

※ 본 프로젝트는 Unity 엔진 기반으로 제작된 2D 액션 플랫폼 게임 프로젝트입니다.  
어둡고 몰입감 있는 분위기 속에서 플레이어가 여러 구역을 탐험하며 적과 보스를 물리치고, 스토리 기반의 챕터를 진행하는 구조로 구성되어 있습니다.

---

## 📌 프로젝트 소개

### 프로젝트 개요
* **스토리**: 가족을 잃은 주인공이 분노와 복수심으로 적들에게 향하는 여정입니다.
* **장르/시점**: 2D 횡스크롤 액션 플랫포머 게임입니다.
* **핵심 그래픽**: 암울한 배경과 정교한 도트 그래픽이 특징입니다.
* **클리어 조건**: 게임 마지막 챕터의 최종 보스를 처치하면 클리어됩니다.

### 주요 기능
* **플레이어 액션**: 좌우 이동, 점프, 더블 점프, 무적 프레임을 이용한 대시 및 공격 콤보 시스템을 제공합니다.
* **전투 및 AI 시스템**: 체력/스탯 기반 전투 시스템 및 고유한 패턴을 가진 적 AI와 보스 AI를 구현했습니다.
* **보스 처치 보상**: 각 지역의 보스를 격파하면 캐릭터의 체력 +50, 공격력 +10이 영구적으로 강화됩니다.
* **체크포인트 시스템**: 사망 시 별도의 저장 없이 해당 지역 시작점이나 보스전 직전의 체크포인트로 돌아갑니다.
* **UI 및 편의 기능**: 일시정지, 옵션 메뉴, 사용자 편의를 위한 키 재설정 기능을 지원합니다.

<img width="338" height="195" alt="image" src="https://github.com/user-attachments/assets/fbf28220-cbbd-4735-9fd5-ca5b63260c07" />

---

## 🕹️ 메뉴 구성 및 챕터 안내

### 🗺️ 챕터별 디자인 구성
게임은 보스가 존재하는 총 3개의 지역을 기반으로 움직이며, 각 지역의 몬스터는 테마에 맞는 독창적인 컨셉과 패턴을 가집니다.

| 챕터 1 (구동현 담당) | 챕터 2 (박민준 담당) | 챕터 3 (전승재 담당) |
| :---: | :---: | :---: |
| <img width="344" height="195" alt="image" src="https://github.com/user-attachments/assets/20e3a880-ce10-47bc-a6f9-3b079bb4d589" />
| <img width="341" height="193" alt="image" src="https://github.com/user-attachments/assets/499924f3-10ae-464a-9119-c1ade0778cf5" />
| <img width="345" height="193" alt="image" src="https://github.com/user-attachments/assets/5199d5b3-da82-433d-b283-e49a3cc775c9" />
|
| 숲 및 동굴 테마 디자인 | 성 및 감옥 내부 UI 디자인 | 최종 보스 방 및 플레이어 액션 |

---

## 🎮 조작 방법 (Controls)

| 행동 | 단축키 | 비고 |
| :--- | :--- | :--- |
| **이동** | 방향키 또는 `A` / `D` | 좌우 이동 |
| **점프** | `Z` | 더블 점프 가능 |
| **공격** | `X` | 타이밍에 따른 콤보 연계 |
| **대시** | `Left Shift` | 무적 프레임을 이용한 패턴 회피 |
| **일시정지** | `Esc` | 옵션 및 키 설정 진입 가능 |

---

## 📁 폴더 구조 (Folder Structure)

* **DarkRevenger/Assets**: 게임 리소스, 씬(Scene 1~3), C# 스크립트 코드
* **DarkRevenger/ProjectSettings**: Unity 프로젝트 환경 설정 파일

---

## 🚀 실행 및 개발 환경 설정

### 1. 릴리즈(Releases) 빌드 파일로 실행하기 (⭐ 권장 및 추천)
현재 원본 소스 코드의 일부 대용량 에셋들이 `.gitignore` 설정으로 인해 제외되어 있습니다. 따라서 **Unity 에디터로 구동 시 리소스 유실로 인해 정상적인 실행이 불가능할 수 있으므로, 게임 플레이를 원하신다면 아래 빌드 파일 실행 방식을 권장합니다.**

1. 본 GitHub 저장소 우측의 **Releases** 메뉴를 클릭하여 이동합니다.
2. 등록된 **"DarkRevenger v1"** 릴리즈의 **Assets** 영역에서 **"v1.zip"** 파일을 다운로드합니다.
3. 다운로드가 완료되면 압축 파일의 **압축을 완전히 해제**합니다.
4. 해제된 폴더 내부에서 게임 실행 파일(`.exe`)을 더블클릭하면 모든 에셋이 온전히 포함된 게임을 바로 플레이하실 수 있습니다.
   * *주의: 폴더 내의 리소스 파일들이 함께 있어야 정상 구동되므로, 실행 파일만 별도로 다른 곳으로 옮기지 마세요.*

### 2. Unity 에디터에서 소스 참고하기
개발 및 에디터 상에서 스크립트나 프로젝트 구조를 참고하려는 경우 아래 내용을 확인해 주세요.

* **권장 Unity 버전**: `v2022.3.23f1`
* **주의사항**: 일부 그래픽 및 사운드 에셋이 이그노어(Ignore) 처리되어 있으므로, 온전한 프로젝트 구동을 위해서는 제외된 에셋 리소스를 수동으로 임포트해야 할 수 있습니다.
* **구동 방법**:
  1. 본 리포지토리를 클론(Clone) 하거나 Zip 파일로 다운로드합니다.
     ```bash
     git clone [https://github.com/JuneSeungJae/DarkRevenger-Final.git](https://github.com/JuneSeungJae/DarkRevenger-Final.git)
     ```
  2. **Unity Hub**를 실행하고 `DarkRevenger` 프로젝트 폴더를 추가하여 활성화합니다.

---

## 👥 팀원 및 역할 (Team & Roles)

**Team: 프로젝트 PJK**

* **전승재 (팀장)**: 게임 전체 기획, 챕터 3 디자인, 액션 시스템 및 플레이어 스크립트 개발
* **구동현 (팀원)**: 챕터 1 디자인, 액션 시스템 및 카메라 스크립트 개발
* **박민준 (팀원)**: 챕터 2 디자인 및 게임 내부 UI 개발

---
_본 프로젝트는 대구가톨릭대학교 SW중심대학사업단 SW캡스톤디자인(소프트웨어 창의설계2) 과제의 일환으로 수행되었으며, Unity 2D 게임 개발 학습 및 포트폴리오용으로 구성된 작품입니다._
