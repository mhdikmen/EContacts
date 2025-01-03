using Mapster;

namespace BuildingBlocks.Dtos
{
    /// <summary>
    /// Example of a base DTO for further proccessing
    /// MHD
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract record BaseDto<TDto, TEntity> : IRegister
    {
        public TEntity ToEntity()
        {
            return this.Adapt<TEntity>();
        }

        public TEntity ToEntity(TEntity entity)
        {
            return (this).Adapt(entity);
        }

        public static TDto FromEntity(TEntity entity)
        {
            return entity.Adapt<TDto>();
        }


        private  TypeAdapterConfig Config { get; set; }

        public virtual void AddCustomMappings() { }


        protected TypeAdapterSetter<TDto, TEntity> SetCustomMappings()
            => Config.ForType<TDto, TEntity>();

        protected TypeAdapterSetter<TEntity, TDto> SetCustomMappingsInverse()
            => Config.ForType<TEntity, TDto>();

        public void Register(TypeAdapterConfig config)
        {
            Config = config;
            AddCustomMappings();
        }
    }
}
