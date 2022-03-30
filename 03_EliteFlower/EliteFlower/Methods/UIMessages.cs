namespace EliteFlower.Methods
{
    public static class UIMessages
    {
        /// <summary>
        /// Retorna un texto dependiendo del idioma en que se pida.
        /// </summary>
        /// <param name="index">El indice del texto</param>
        /// <param name="english">Si es en ingles/espanol</param>
        /// <returns>Devuelve el texto pedido</returns>
        public static string Package(int index, bool english)
        {
            string msg = "NaN";
            if (english)
            {
                switch (index)
                {
                    case 01: msg = Properties.ResourceMsg_en_US.T124; break;
                    case 02: msg = Properties.ResourceMsg_en_US.T122; break;
                    case 03: msg = Properties.ResourceMsg_en_US.T082; break;
                    case 04: msg = Properties.ResourceMsg_en_US.T083; break;
                    case 05: msg = Properties.ResourceMsg_en_US.T084; break;
                    case 06: msg = Properties.ResourceMsg_en_US.T085; break;
                    case 07: msg = Properties.ResourceMsg_en_US.T125; break;
                    case 08: msg = Properties.ResourceMsg_en_US.T126; break;
                    case 09: msg = Properties.ResourceMsg_en_US.T127; break;
                    case 10: msg = Properties.ResourceMsg_en_US.T123; break;
                    case 11: msg = Properties.ResourceMsg_en_US.T137; break;
                    case 12: msg = Properties.ResourceMsg_en_US.T138; break;
                    case 13: msg = Properties.ResourceMsg_en_US.T139; break;
                    case 14: msg = Properties.ResourceMsg_en_US.T140; break;
                    case 15: msg = Properties.ResourceMsg_en_US.T141; break;
                    case 16: msg = Properties.ResourceMsg_en_US.T142; break;
                    case 17: msg = Properties.ResourceMsg_en_US.T143; break;
                    case 18: msg = Properties.ResourceMsg_en_US.T001; break;
                    default: break;
                }
            }
            else
            {
                switch (index)
                {
                    case 01: msg = Properties.ResourceMsg_es_CO.T124; break;
                    case 02: msg = Properties.ResourceMsg_es_CO.T122; break;
                    case 03: msg = Properties.ResourceMsg_es_CO.T082; break;
                    case 04: msg = Properties.ResourceMsg_es_CO.T083; break;
                    case 05: msg = Properties.ResourceMsg_es_CO.T084; break;
                    case 06: msg = Properties.ResourceMsg_es_CO.T085; break;
                    case 07: msg = Properties.ResourceMsg_es_CO.T125; break;
                    case 08: msg = Properties.ResourceMsg_es_CO.T126; break;
                    case 09: msg = Properties.ResourceMsg_es_CO.T127; break;
                    case 10: msg = Properties.ResourceMsg_es_CO.T123; break;
                    case 11: msg = Properties.ResourceMsg_es_CO.T137; break;
                    case 12: msg = Properties.ResourceMsg_es_CO.T138; break;
                    case 13: msg = Properties.ResourceMsg_es_CO.T139; break;
                    case 14: msg = Properties.ResourceMsg_es_CO.T140; break;
                    case 15: msg = Properties.ResourceMsg_es_CO.T141; break;
                    case 16: msg = Properties.ResourceMsg_es_CO.T142; break;
                    case 17: msg = Properties.ResourceMsg_es_CO.T143; break;
                    case 18: msg = Properties.ResourceMsg_es_CO.T001; break;
                    default: break;
                }
            }
            return msg;
        }
        /// <summary>
        /// Retorna un texto dependiendo del idioma en que se pida.
        /// </summary>
        /// <param name="index">El indice del texto</param>
        /// <param name="english">Si es en ingles/espanol</param>
        /// <returns>Devuelve el texto pedido</returns>
        public static string AddOn(int index, bool english)
        {
            string msg = "NaN";
            if (english)
            {
                switch (index)
                {
                    case 01: msg = Properties.ResourceMsg_en_US.T082; break;
                    case 02: msg = Properties.ResourceMsg_en_US.T083; break;
                    case 03: msg = Properties.ResourceMsg_en_US.T084; break;
                    case 04: msg = Properties.ResourceMsg_en_US.T085; break;
                    case 05: msg = Properties.ResourceMsg_en_US.T122; break;
                    case 06: msg = Properties.ResourceMsg_en_US.T123; break;
                    case 07: msg = Properties.ResourceMsg_en_US.T124; break;
                    case 08: msg = Properties.ResourceMsg_en_US.T125; break;
                    case 09: msg = Properties.ResourceMsg_en_US.T126; break;
                    case 10: msg = Properties.ResourceMsg_en_US.T127; break;
                    case 11: msg = Properties.ResourceMsg_en_US.T129; break;
                    case 12: msg = Properties.ResourceMsg_en_US.T130; break;
                    case 13: msg = Properties.ResourceMsg_en_US.T131; break;
                    case 14: msg = Properties.ResourceMsg_en_US.T132; break;
                    case 15: msg = Properties.ResourceMsg_en_US.T133; break;
                    case 16: msg = Properties.ResourceMsg_en_US.T134; break;
                    case 17: msg = Properties.ResourceMsg_en_US.T135; break;
                    case 18: msg = Properties.ResourceMsg_en_US.T136; break;
                    case 19: msg = Properties.ResourceMsg_en_US.T001; break;
                    default: break;
                }
            }
            else
            {
                switch (index)
                {
                    case 01: msg = Properties.ResourceMsg_es_CO.T082; break;
                    case 02: msg = Properties.ResourceMsg_es_CO.T083; break;
                    case 03: msg = Properties.ResourceMsg_es_CO.T084; break;
                    case 04: msg = Properties.ResourceMsg_es_CO.T085; break;
                    case 05: msg = Properties.ResourceMsg_es_CO.T122; break;
                    case 06: msg = Properties.ResourceMsg_es_CO.T123; break;
                    case 07: msg = Properties.ResourceMsg_es_CO.T124; break;
                    case 08: msg = Properties.ResourceMsg_es_CO.T125; break;
                    case 09: msg = Properties.ResourceMsg_es_CO.T126; break;
                    case 10: msg = Properties.ResourceMsg_es_CO.T127; break;
                    case 11: msg = Properties.ResourceMsg_es_CO.T129; break;
                    case 12: msg = Properties.ResourceMsg_es_CO.T130; break;
                    case 13: msg = Properties.ResourceMsg_es_CO.T131; break;
                    case 14: msg = Properties.ResourceMsg_es_CO.T132; break;
                    case 15: msg = Properties.ResourceMsg_es_CO.T133; break;
                    case 16: msg = Properties.ResourceMsg_es_CO.T134; break;
                    case 17: msg = Properties.ResourceMsg_es_CO.T135; break;
                    case 18: msg = Properties.ResourceMsg_es_CO.T136; break;
                    case 19: msg = Properties.ResourceMsg_es_CO.T001; break;
                    default: break;
                }
            }
            return msg;
        }
        /// <summary>
        /// Retorna un texto dependiendo del idioma en que se pida.
        /// </summary>
        /// <param name="index">El indice del texto</param>
        /// <param name="english">Si es en ingles/espanol</param>
        /// <returns>Devuelve el texto pedido</returns>
        public static string AddProduct(int index, bool english)
        {
            string msg = "NaN";
            if (english)
            {
                switch (index)
                {
                    case 01: msg = Properties.ResourceMsg_en_US.T001; break;
                    case 02: msg = Properties.ResourceMsg_en_US.T057; break;
                    case 03: msg = Properties.ResourceMsg_en_US.T058; break;
                    case 04: msg = Properties.ResourceMsg_en_US.T059; break;
                    case 05: msg = Properties.ResourceMsg_en_US.T060; break;
                    case 06: msg = Properties.ResourceMsg_en_US.T061; break;
                    case 07: msg = Properties.ResourceMsg_en_US.T062; break;
                    case 08: msg = Properties.ResourceMsg_en_US.T063; break;
                    case 09: msg = Properties.ResourceMsg_en_US.T064; break;
                    case 10: msg = Properties.ResourceMsg_en_US.T065; break;
                    case 11: msg = Properties.ResourceMsg_en_US.T066; break;
                    case 12: msg = Properties.ResourceMsg_en_US.T067; break;
                    case 13: msg = Properties.ResourceMsg_en_US.T068; break;
                    case 14: msg = Properties.ResourceMsg_en_US.T069; break;
                    case 15: msg = Properties.ResourceMsg_en_US.T070; break;
                    case 16: msg = Properties.ResourceMsg_en_US.T079; break;
                    case 17: msg = Properties.ResourceMsg_en_US.T080; break;
                    case 18: msg = Properties.ResourceMsg_en_US.T082; break;
                    case 19: msg = Properties.ResourceMsg_en_US.T083; break;
                    case 20: msg = Properties.ResourceMsg_en_US.T084; break;
                    case 21: msg = Properties.ResourceMsg_en_US.T085; break;
                    case 22: msg = Properties.ResourceMsg_en_US.T144; break;
                    default: break;
                }
            }
            else
            {
                switch (index)
                {
                    case 01: msg = Properties.ResourceMsg_es_CO.T001; break;
                    case 02: msg = Properties.ResourceMsg_es_CO.T057; break;
                    case 03: msg = Properties.ResourceMsg_es_CO.T058; break;
                    case 04: msg = Properties.ResourceMsg_es_CO.T059; break;
                    case 05: msg = Properties.ResourceMsg_es_CO.T060; break;
                    case 06: msg = Properties.ResourceMsg_es_CO.T061; break;
                    case 07: msg = Properties.ResourceMsg_es_CO.T062; break;
                    case 08: msg = Properties.ResourceMsg_es_CO.T063; break;
                    case 09: msg = Properties.ResourceMsg_es_CO.T064; break;
                    case 10: msg = Properties.ResourceMsg_es_CO.T065; break;
                    case 11: msg = Properties.ResourceMsg_es_CO.T066; break;
                    case 12: msg = Properties.ResourceMsg_es_CO.T067; break;
                    case 13: msg = Properties.ResourceMsg_es_CO.T068; break;
                    case 14: msg = Properties.ResourceMsg_es_CO.T069; break;
                    case 15: msg = Properties.ResourceMsg_es_CO.T070; break;
                    case 16: msg = Properties.ResourceMsg_es_CO.T079; break;
                    case 17: msg = Properties.ResourceMsg_es_CO.T080; break;
                    case 18: msg = Properties.ResourceMsg_es_CO.T082; break;
                    case 19: msg = Properties.ResourceMsg_es_CO.T083; break;
                    case 20: msg = Properties.ResourceMsg_es_CO.T084; break;
                    case 21: msg = Properties.ResourceMsg_es_CO.T085; break;
                    case 22: msg = Properties.ResourceMsg_es_CO.T144; break;
                    default: break;
                }
            }
            return msg;
        }
        /// <summary>
        /// Retorna un texto dependiendo del idioma en que se pida.
        /// </summary>
        /// <param name="index">El indice del texto</param>
        /// <param name="english">Si es en ingles/espanol</param>
        /// <returns>Devuelve el texto pedido</returns>
        public static string Checker(int index, bool english)
        {
            string msg = "NaN";
            if (english)
            {
                switch (index)
                {
                    case 01: msg = Properties.ResourceMsg_en_US.T001; break;
                    case 02: msg = Properties.ResourceMsg_en_US.T071; break;
                    case 03: msg = Properties.ResourceMsg_en_US.T072; break;
                    case 04: msg = Properties.ResourceMsg_en_US.T073; break;
                    case 05: msg = Properties.ResourceMsg_en_US.T085; break;
                    default: break;
                }
            }
            else
            {
                switch (index)
                {
                    case 01: msg = Properties.ResourceMsg_es_CO.T001; break;
                    case 02: msg = Properties.ResourceMsg_es_CO.T071; break;
                    case 03: msg = Properties.ResourceMsg_es_CO.T072; break;
                    case 04: msg = Properties.ResourceMsg_es_CO.T073; break;
                    case 05: msg = Properties.ResourceMsg_es_CO.T085; break;
                    default: break;
                }
            }
            return msg;
        }
        /// <summary>
        /// Retorna un texto dependiendo del idioma en que se pida.
        /// </summary>
        /// <param name="index">El indice del texto</param>
        /// <param name="english">Si es en ingles/espanol</param>
        /// <returns>Devuelve el texto pedido</returns>
        public static string EliteFlower(int index, bool english)
        {
            string msg = "NaN";
            if (english)
            {
                switch (index)
                {
                    case 01: msg = Properties.ResourceMsg_en_US.T001; break;
                    case 02: msg = Properties.ResourceMsg_en_US.T002; break;
                    case 03: msg = Properties.ResourceMsg_en_US.T003; break;
                    case 04: msg = Properties.ResourceMsg_en_US.T004; break;
                    case 05: msg = Properties.ResourceMsg_en_US.T005; break;
                    case 06: msg = Properties.ResourceMsg_en_US.T006; break;
                    case 07: msg = Properties.ResourceMsg_en_US.T007; break;
                    case 08: msg = Properties.ResourceMsg_en_US.T008; break;
                    case 09: msg = Properties.ResourceMsg_en_US.T009; break;
                    case 10: msg = Properties.ResourceMsg_en_US.T010; break;
                    case 11: msg = Properties.ResourceMsg_en_US.T011; break;
                    case 12: msg = Properties.ResourceMsg_en_US.T012; break;
                    case 13: msg = Properties.ResourceMsg_en_US.T013; break;
                    case 14: msg = Properties.ResourceMsg_en_US.T014; break;
                    case 15: msg = Properties.ResourceMsg_en_US.T015; break;
                    case 16: msg = Properties.ResourceMsg_en_US.T016; break;
                    case 17: msg = Properties.ResourceMsg_en_US.T017; break;
                    case 18: msg = Properties.ResourceMsg_en_US.T018; break;
                    case 19: msg = Properties.ResourceMsg_en_US.T019; break;
                    case 20: msg = Properties.ResourceMsg_en_US.T020; break;
                    case 21: msg = Properties.ResourceMsg_en_US.T021; break;
                    case 22: msg = Properties.ResourceMsg_en_US.T022; break;
                    case 23: msg = Properties.ResourceMsg_en_US.T023; break;
                    case 24: msg = Properties.ResourceMsg_en_US.T024; break;
                    case 25: msg = Properties.ResourceMsg_en_US.T025; break;
                    case 26: msg = Properties.ResourceMsg_en_US.T026; break;
                    case 27: msg = Properties.ResourceMsg_en_US.T027; break;
                    case 28: msg = Properties.ResourceMsg_en_US.T028; break;
                    case 29: msg = Properties.ResourceMsg_en_US.T029; break;
                    case 30: msg = Properties.ResourceMsg_en_US.T030; break;
                    case 31: msg = Properties.ResourceMsg_en_US.T031; break;
                    case 32: msg = Properties.ResourceMsg_en_US.T032; break;
                    case 33: msg = Properties.ResourceMsg_en_US.T033; break;
                    case 34: msg = Properties.ResourceMsg_en_US.T034; break;
                    case 35: msg = Properties.ResourceMsg_en_US.T035; break;
                    case 36: msg = Properties.ResourceMsg_en_US.T036; break;
                    case 37: msg = Properties.ResourceMsg_en_US.T037; break;
                    case 38: msg = Properties.ResourceMsg_en_US.T038; break;
                    case 39: msg = Properties.ResourceMsg_en_US.T039; break;
                    case 40: msg = Properties.ResourceMsg_en_US.T040; break;
                    case 41: msg = Properties.ResourceMsg_en_US.T041; break;
                    case 42: msg = Properties.ResourceMsg_en_US.T042; break;
                    case 43: msg = Properties.ResourceMsg_en_US.T043; break;
                    case 44: msg = Properties.ResourceMsg_en_US.T044; break;
                    case 45: msg = Properties.ResourceMsg_en_US.T045; break;
                    case 46: msg = Properties.ResourceMsg_en_US.T046; break;
                    case 47: msg = Properties.ResourceMsg_en_US.T047; break;
                    case 48: msg = Properties.ResourceMsg_en_US.T048; break;
                    case 49: msg = Properties.ResourceMsg_en_US.T049; break;
                    case 50: msg = Properties.ResourceMsg_en_US.T050; break;
                    case 51: msg = Properties.ResourceMsg_en_US.T051; break;
                    case 52: msg = Properties.ResourceMsg_en_US.T052; break;
                    case 53: msg = Properties.ResourceMsg_en_US.T053; break;
                    case 54: msg = Properties.ResourceMsg_en_US.T054; break;
                    case 55: msg = Properties.ResourceMsg_en_US.T081; break;
                    case 56: msg = Properties.ResourceMsg_en_US.T082; break;
                    case 57: msg = Properties.ResourceMsg_en_US.T083; break;
                    case 58: msg = Properties.ResourceMsg_en_US.T084; break;
                    case 59: msg = Properties.ResourceMsg_en_US.T085; break;
                    case 60: msg = Properties.ResourceMsg_en_US.T086; break;
                    case 61: msg = Properties.ResourceMsg_en_US.T087; break;
                    case 62: msg = Properties.ResourceMsg_en_US.T088; break;
                    case 63: msg = Properties.ResourceMsg_en_US.T089; break;
                    case 64: msg = Properties.ResourceMsg_en_US.T090; break;
                    case 65: msg = Properties.ResourceMsg_en_US.T091; break;
                    case 66: msg = Properties.ResourceMsg_en_US.T092; break;
                    case 67: msg = Properties.ResourceMsg_en_US.T093; break;
                    case 68: msg = Properties.ResourceMsg_en_US.T094; break;
                    case 69: msg = Properties.ResourceMsg_en_US.T095; break;
                    case 70: msg = Properties.ResourceMsg_en_US.T096; break;
                    case 71: msg = Properties.ResourceMsg_en_US.T097; break;
                    case 72: msg = Properties.ResourceMsg_en_US.T098; break;
                    case 73: msg = Properties.ResourceMsg_en_US.T099; break;
                    case 74: msg = Properties.ResourceMsg_en_US.T100; break;
                    case 75: msg = Properties.ResourceMsg_en_US.T101; break;
                    case 76: msg = Properties.ResourceMsg_en_US.T128; break;
                    case 77: msg = Properties.ResourceMsg_en_US.T145; break;
                    case 78: msg = Properties.ResourceMsg_en_US.T146; break;
                    case 79: msg = Properties.ResourceMsg_en_US.T147; break;
                    case 80: msg = Properties.ResourceMsg_en_US.T148; break;
                    case 81: msg = Properties.ResourceMsg_en_US.T149; break;
                    case 82: msg = Properties.ResourceMsg_en_US.T150; break;
                    case 83: msg = Properties.ResourceMsg_en_US.T151; break;
                    case 84: msg = Properties.ResourceMsg_en_US.T152; break;
                    case 85: msg = Properties.ResourceMsg_en_US.T153; break;
                    case 86: msg = Properties.ResourceMsg_en_US.T154; break;
                    case 87: msg = Properties.ResourceMsg_en_US.T155; break;
                    case 88: msg = Properties.ResourceMsg_en_US.T156; break;
                    case 89: msg = Properties.ResourceMsg_en_US.T157; break;
                    case 90: msg = Properties.ResourceMsg_en_US.T158; break;
                    case 91: msg = Properties.ResourceMsg_en_US.T159; break;
                    case 92: msg = Properties.ResourceMsg_en_US.T160; break;
                    case 93: msg = Properties.ResourceMsg_en_US.T161; break;
                    case 94: msg = Properties.ResourceMsg_en_US.T162; break;
                    case 95: msg = Properties.ResourceMsg_en_US.T163; break;
                    case 96: msg = Properties.ResourceMsg_en_US.T164; break;
                    case 97: msg = Properties.ResourceMsg_en_US.T165; break;
                    case 98: msg = Properties.ResourceMsg_en_US.T166; break;
                    case 99: msg = Properties.ResourceMsg_en_US.T167; break;
                    case 100: msg = Properties.ResourceMsg_en_US.T168; break;
                    case 101: msg = Properties.ResourceMsg_en_US.T169; break;
                    case 102: msg = Properties.ResourceMsg_en_US.T170; break;
                    case 103: msg = Properties.ResourceMsg_en_US.T171; break;
                    case 104: msg = Properties.ResourceMsg_en_US.T172; break;
                    case 105: msg = Properties.ResourceMsg_en_US.T173; break;
                    case 106: msg = Properties.ResourceMsg_en_US.T174; break;
                    default: break;
                }
            }
            else
            {
                switch (index)
                {
                    case 01: msg = Properties.ResourceMsg_es_CO.T001; break;
                    case 02: msg = Properties.ResourceMsg_es_CO.T002; break;
                    case 03: msg = Properties.ResourceMsg_es_CO.T003; break;
                    case 04: msg = Properties.ResourceMsg_es_CO.T004; break;
                    case 05: msg = Properties.ResourceMsg_es_CO.T005; break;
                    case 06: msg = Properties.ResourceMsg_es_CO.T006; break;
                    case 07: msg = Properties.ResourceMsg_es_CO.T007; break;
                    case 08: msg = Properties.ResourceMsg_es_CO.T008; break;
                    case 09: msg = Properties.ResourceMsg_es_CO.T009; break;
                    case 10: msg = Properties.ResourceMsg_es_CO.T010; break;
                    case 11: msg = Properties.ResourceMsg_es_CO.T011; break;
                    case 12: msg = Properties.ResourceMsg_es_CO.T012; break;
                    case 13: msg = Properties.ResourceMsg_es_CO.T013; break;
                    case 14: msg = Properties.ResourceMsg_es_CO.T014; break;
                    case 15: msg = Properties.ResourceMsg_es_CO.T015; break;
                    case 16: msg = Properties.ResourceMsg_es_CO.T016; break;
                    case 17: msg = Properties.ResourceMsg_es_CO.T017; break;
                    case 18: msg = Properties.ResourceMsg_es_CO.T018; break;
                    case 19: msg = Properties.ResourceMsg_es_CO.T019; break;
                    case 20: msg = Properties.ResourceMsg_es_CO.T020; break;
                    case 21: msg = Properties.ResourceMsg_es_CO.T021; break;
                    case 22: msg = Properties.ResourceMsg_es_CO.T022; break;
                    case 23: msg = Properties.ResourceMsg_es_CO.T023; break;
                    case 24: msg = Properties.ResourceMsg_es_CO.T024; break;
                    case 25: msg = Properties.ResourceMsg_es_CO.T025; break;
                    case 26: msg = Properties.ResourceMsg_es_CO.T026; break;
                    case 27: msg = Properties.ResourceMsg_es_CO.T027; break;
                    case 28: msg = Properties.ResourceMsg_es_CO.T028; break;
                    case 29: msg = Properties.ResourceMsg_es_CO.T029; break;
                    case 30: msg = Properties.ResourceMsg_es_CO.T030; break;
                    case 31: msg = Properties.ResourceMsg_es_CO.T031; break;
                    case 32: msg = Properties.ResourceMsg_es_CO.T032; break;
                    case 33: msg = Properties.ResourceMsg_es_CO.T033; break;
                    case 34: msg = Properties.ResourceMsg_es_CO.T034; break;
                    case 35: msg = Properties.ResourceMsg_es_CO.T035; break;
                    case 36: msg = Properties.ResourceMsg_es_CO.T036; break;
                    case 37: msg = Properties.ResourceMsg_es_CO.T037; break;
                    case 38: msg = Properties.ResourceMsg_es_CO.T038; break;
                    case 39: msg = Properties.ResourceMsg_es_CO.T039; break;
                    case 40: msg = Properties.ResourceMsg_es_CO.T040; break;
                    case 41: msg = Properties.ResourceMsg_es_CO.T041; break;
                    case 42: msg = Properties.ResourceMsg_es_CO.T042; break;
                    case 43: msg = Properties.ResourceMsg_es_CO.T043; break;
                    case 44: msg = Properties.ResourceMsg_es_CO.T044; break;
                    case 45: msg = Properties.ResourceMsg_es_CO.T045; break;
                    case 46: msg = Properties.ResourceMsg_es_CO.T046; break;
                    case 47: msg = Properties.ResourceMsg_es_CO.T047; break;
                    case 48: msg = Properties.ResourceMsg_es_CO.T048; break;
                    case 49: msg = Properties.ResourceMsg_es_CO.T049; break;
                    case 50: msg = Properties.ResourceMsg_es_CO.T050; break;
                    case 51: msg = Properties.ResourceMsg_es_CO.T051; break;
                    case 52: msg = Properties.ResourceMsg_es_CO.T052; break;
                    case 53: msg = Properties.ResourceMsg_es_CO.T053; break;
                    case 54: msg = Properties.ResourceMsg_es_CO.T054; break;
                    case 55: msg = Properties.ResourceMsg_es_CO.T081; break;
                    case 56: msg = Properties.ResourceMsg_es_CO.T082; break;
                    case 57: msg = Properties.ResourceMsg_es_CO.T083; break;
                    case 58: msg = Properties.ResourceMsg_es_CO.T084; break;
                    case 59: msg = Properties.ResourceMsg_es_CO.T085; break;
                    case 60: msg = Properties.ResourceMsg_es_CO.T086; break;
                    case 61: msg = Properties.ResourceMsg_es_CO.T087; break;
                    case 62: msg = Properties.ResourceMsg_es_CO.T088; break;
                    case 63: msg = Properties.ResourceMsg_es_CO.T089; break;
                    case 64: msg = Properties.ResourceMsg_es_CO.T090; break;
                    case 65: msg = Properties.ResourceMsg_es_CO.T091; break;
                    case 66: msg = Properties.ResourceMsg_es_CO.T092; break;
                    case 67: msg = Properties.ResourceMsg_es_CO.T093; break;
                    case 68: msg = Properties.ResourceMsg_es_CO.T094; break;
                    case 69: msg = Properties.ResourceMsg_es_CO.T095; break;
                    case 70: msg = Properties.ResourceMsg_es_CO.T096; break;
                    case 71: msg = Properties.ResourceMsg_es_CO.T097; break;
                    case 72: msg = Properties.ResourceMsg_es_CO.T098; break;
                    case 73: msg = Properties.ResourceMsg_es_CO.T099; break;
                    case 74: msg = Properties.ResourceMsg_es_CO.T100; break;
                    case 75: msg = Properties.ResourceMsg_es_CO.T101; break;
                    case 76: msg = Properties.ResourceMsg_es_CO.T128; break;
                    case 77: msg = Properties.ResourceMsg_es_CO.T145; break;
                    case 78: msg = Properties.ResourceMsg_es_CO.T146; break;
                    case 79: msg = Properties.ResourceMsg_es_CO.T147; break;
                    case 80: msg = Properties.ResourceMsg_es_CO.T148; break;
                    case 81: msg = Properties.ResourceMsg_es_CO.T149; break;
                    case 82: msg = Properties.ResourceMsg_es_CO.T150; break;
                    case 83: msg = Properties.ResourceMsg_es_CO.T151; break;
                    case 84: msg = Properties.ResourceMsg_es_CO.T152; break;
                    case 85: msg = Properties.ResourceMsg_es_CO.T153; break;
                    case 86: msg = Properties.ResourceMsg_es_CO.T154; break;
                    case 87: msg = Properties.ResourceMsg_es_CO.T155; break;
                    case 88: msg = Properties.ResourceMsg_es_CO.T156; break;
                    case 89: msg = Properties.ResourceMsg_es_CO.T157; break;
                    case 90: msg = Properties.ResourceMsg_es_CO.T158; break;
                    case 91: msg = Properties.ResourceMsg_es_CO.T159; break;
                    case 92: msg = Properties.ResourceMsg_es_CO.T160; break;
                    case 93: msg = Properties.ResourceMsg_es_CO.T161; break;
                    case 94: msg = Properties.ResourceMsg_es_CO.T162; break;
                    case 95: msg = Properties.ResourceMsg_es_CO.T163; break;
                    case 96: msg = Properties.ResourceMsg_es_CO.T164; break;
                    case 97: msg = Properties.ResourceMsg_es_CO.T165; break;
                    case 98: msg = Properties.ResourceMsg_es_CO.T166; break;
                    case 99: msg = Properties.ResourceMsg_es_CO.T167; break;
                    case 100: msg = Properties.ResourceMsg_es_CO.T168; break;
                    case 101: msg = Properties.ResourceMsg_es_CO.T169; break;
                    case 102: msg = Properties.ResourceMsg_es_CO.T170; break;
                    case 103: msg = Properties.ResourceMsg_es_CO.T171; break;
                    case 104: msg = Properties.ResourceMsg_es_CO.T172; break;
                    case 105: msg = Properties.ResourceMsg_es_CO.T173; break;
                    case 106: msg = Properties.ResourceMsg_es_CO.T174; break;
                    default: break;
                }
            }
            return msg;
        }
        /// <summary>
        /// Retorna un texto dependiendo del idioma en que se pida.
        /// </summary>
        /// <param name="index">El indice del texto</param>
        /// <param name="english">Si es en ingles/espanol</param>
        /// <returns>Devuelve el texto pedido</returns>
        public static string Overview(int index, bool english)
        {
            string msg = "NaN";
            if (english)
            {
                switch (index)
                {
                    case 01: msg = Properties.ResourceMsg_en_US.T102; break;
                    case 02: msg = Properties.ResourceMsg_en_US.T103; break;
                    case 03: msg = Properties.ResourceMsg_en_US.T104; break;
                    case 04: msg = Properties.ResourceMsg_en_US.T105; break;
                    case 05: msg = Properties.ResourceMsg_en_US.T106; break;
                    case 06: msg = Properties.ResourceMsg_en_US.T107; break;
                    case 07: msg = Properties.ResourceMsg_en_US.T108; break;
                    case 08: msg = Properties.ResourceMsg_en_US.T109; break;
                    case 09: msg = Properties.ResourceMsg_en_US.T110; break;
                    case 10: msg = Properties.ResourceMsg_en_US.T111; break;
                    case 11: msg = Properties.ResourceMsg_en_US.T112; break;
                    case 12: msg = Properties.ResourceMsg_en_US.T113; break;
                    case 13: msg = Properties.ResourceMsg_en_US.T114; break;
                    case 14: msg = Properties.ResourceMsg_en_US.T115; break;
                    case 15: msg = Properties.ResourceMsg_en_US.T116; break;
                    case 16: msg = Properties.ResourceMsg_en_US.T117; break;
                    case 17: msg = Properties.ResourceMsg_en_US.T118; break;
                    default: break;
                }
            }
            else
            {
                switch (index)
                {
                    case 01: msg = Properties.ResourceMsg_es_CO.T102; break;
                    case 02: msg = Properties.ResourceMsg_es_CO.T103; break;
                    case 03: msg = Properties.ResourceMsg_es_CO.T104; break;
                    case 04: msg = Properties.ResourceMsg_es_CO.T105; break;
                    case 05: msg = Properties.ResourceMsg_es_CO.T106; break;
                    case 06: msg = Properties.ResourceMsg_es_CO.T107; break;
                    case 07: msg = Properties.ResourceMsg_es_CO.T108; break;
                    case 08: msg = Properties.ResourceMsg_es_CO.T109; break;
                    case 09: msg = Properties.ResourceMsg_es_CO.T110; break;
                    case 10: msg = Properties.ResourceMsg_es_CO.T111; break;
                    case 11: msg = Properties.ResourceMsg_es_CO.T112; break;
                    case 12: msg = Properties.ResourceMsg_es_CO.T113; break;
                    case 13: msg = Properties.ResourceMsg_es_CO.T114; break;
                    case 14: msg = Properties.ResourceMsg_es_CO.T115; break;
                    case 15: msg = Properties.ResourceMsg_es_CO.T116; break;
                    case 16: msg = Properties.ResourceMsg_es_CO.T117; break;
                    case 17: msg = Properties.ResourceMsg_es_CO.T118; break;
                    default: break;
                }
            }
            return msg;
        }
        /// <summary>
        /// Retorna un texto dependiendo del idioma en que se pida.
        /// </summary>
        /// <param name="index">El indice del texto</param>
        /// <param name="english">Si es en ingles/espanol</param>
        /// <returns>Devuelve el texto pedido</returns>
        public static string PLC(int index, bool english)
        {
            string msg = "NaN";
            if (english)
            {
                switch (index)
                {
                    case 01: msg = Properties.ResourceMsg_en_US.T001; break;
                    case 02: msg = Properties.ResourceMsg_en_US.T074; break;
                    case 03: msg = Properties.ResourceMsg_en_US.T075; break;
                    case 04: msg = Properties.ResourceMsg_en_US.T076; break;
                    case 05: msg = Properties.ResourceMsg_en_US.T077; break;
                    case 06: msg = Properties.ResourceMsg_en_US.T078; break;
                    case 07: msg = Properties.ResourceMsg_en_US.T082; break;
                    case 08: msg = Properties.ResourceMsg_en_US.T085; break;
                    default: break;
                }
            }
            else
            {
                switch (index)
                {
                    case 01: msg = Properties.ResourceMsg_es_CO.T001; break;
                    case 02: msg = Properties.ResourceMsg_es_CO.T074; break;
                    case 03: msg = Properties.ResourceMsg_es_CO.T075; break;
                    case 04: msg = Properties.ResourceMsg_es_CO.T076; break;
                    case 05: msg = Properties.ResourceMsg_es_CO.T077; break;
                    case 06: msg = Properties.ResourceMsg_es_CO.T078; break;
                    case 07: msg = Properties.ResourceMsg_es_CO.T082; break;
                    case 08: msg = Properties.ResourceMsg_es_CO.T085; break;
                    default: break;
                }
            }
            return msg;
        }
    }
}
