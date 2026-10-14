using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public interface IUseSkill
    {
        UseSkillResultData UseSkill(Player player);
        string getName();
    }

    public enum SkillType
    {
        BattleSkill = 1,
        RescueSkill,
        OtherSkill
    }

    public class UseSkillResultData
    {
        private bool isSuccses = false;
        private int value = 0;
        private string message = "";
        private SkillType skilltype;

        public UseSkillResultData(bool isSuccses, int value, string message, SkillType skilltype)
        {
            this.isSuccses = isSuccses;
            this.value = value;
            this.message = message;
            this.skilltype = skilltype;
        }

        public bool IsSuccses() => isSuccses;
        public int getValue() => value;
        public string getMessage() => message;
        public SkillType GetSkillType() => skilltype;
    }

    public abstract class Skill : NameTrait
    {
        protected int epCost = 0;
        public Skill(string name, int cost)
        {
            this.name = name;
            this.epCost = cost;
        }

        protected bool checkSkill(Player player)
        {
            return player.decreaseEp(this.epCost);
        }

        public int getEpCost() => epCost;
    }

    public class Strike : Skill, IUseSkill
    {
        public Strike() : base("Удар", 5)
        {

        }

        public UseSkillResultData UseSkill(Player player)
        {
            int damage = 0;
            bool isSuccses = checkSkill(player);


            if (isSuccses)
            {
                damage = player.CalculateDamage();
            }

            return new UseSkillResultData(isSuccses, damage, "", SkillType.BattleSkill);
        }
    }

    public class MightyStrike : Skill, IUseSkill
    {
        public MightyStrike() : base("Сильний Удар", 20)
        {

        }

        public UseSkillResultData UseSkill(Player player)
        {
            int damage = 0;
            bool isSuccses = checkSkill(player);

            if (isSuccses)
            {
                damage = player.CalculateDamage() * 2;
            }

            return new UseSkillResultData(isSuccses, damage, "", SkillType.BattleSkill);
        }
    }

    public class Regenration : Skill, IUseSkill
    {
        public Regenration() : base("Регенерація", 60)
        {

        }

        public UseSkillResultData UseSkill(Player player)
        {
            bool isSuccses = checkSkill(player);

            if (isSuccses)
            {
                player.rescueHp(player.getLevel() * 100);
            }

            return new UseSkillResultData(isSuccses, 0, "", SkillType.RescueSkill);
        }
    }

    public class Rest : Skill, IUseSkill
    {
        public Rest() : base("Відпочинок", 0)
        {

        }

        public UseSkillResultData UseSkill(Player player)
        {
            player.increaseEp(30);

            return new UseSkillResultData(true, 0, "", SkillType.OtherSkill);
        }
    }
    public class Iaido : Skill, IUseSkill
    {
        public Iaido() : base("Iaido", 5)
        {

        }

        public UseSkillResultData UseSkill(Player player)
        {
            int damage = 0;
            bool isSuccses = checkSkill(player);

            if (isSuccses)
            {
                damage = player.CalculateDamage() + 50;
            }

            return new UseSkillResultData(isSuccses, damage, "", SkillType.BattleSkill);
        }
    }
    public class Explosion : Skill, IUseSkill
    {
        public Explosion() : base("Вибух", 100)
        {

        }

        public UseSkillResultData UseSkill(Player player)
        {
            int damage = 0;
            bool isSuccses = checkSkill(player);

            if (isSuccses)
            {
                damage = player.CalculateDamage() * 7;
            }

            return new UseSkillResultData(isSuccses, damage, "", SkillType.BattleSkill);
        }
    }
    public class DoubleWeapon : Skill, IUseSkill
    {
        public DoubleWeapon() : base("Подвійна зброя", 100)
        {

        }

        public UseSkillResultData UseSkill(Player player)
        {
            int damage = 0;
            bool isSuccses = checkSkill(player);

            if (isSuccses)
            {
                damage = (player.getWeapon().getharm() * 2) + (player.str * 3);
            }

            return new UseSkillResultData(isSuccses, damage, "", SkillType.BattleSkill);
        }
    }
}
